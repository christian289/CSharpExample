using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace ADODotNetBulkInsert
{
    public class BulkInsertHelper<T>
    {
        public string ConnectionString { get; set; }
        public string TableName { get; set; }
        public IList<T> InternalStore { get; set; }
        public int CommitBatchSize { get; set; } = 3000;

        /// <summary>
        /// CommitBatchSize로 한번에 BulkInsert 할 DataTable의 Row수를 정의한다.
        /// CommitBatchSize로 데이터를 Paging 하는데 나머지가 생길 경우 버릴 수 없으니 1을 추가해준다.
        /// 넘겨진 IList에 대하여 이미 BulkInsert한 부분은 건너뛰어야하므로 넘겨진 페이지 * 페이지 Row 단위만큼 Skip을 사용했고, 한 페이지의 단위(Take)만큼 리턴하여 DataTable로 변환한다.
        /// </summary>
        public void Commit()
        {
            try
            {
                if (InternalStore.Count > 0)
                {
                    DataTable dt;
                    int numberOfPages = (InternalStore.Count / CommitBatchSize) + (InternalStore.Count % CommitBatchSize == 0 ? 0 : 1);

                    for (int pageIndex = 0; pageIndex < numberOfPages; pageIndex++)
                    {
                        dt = InternalStore.Skip(pageIndex * CommitBatchSize).Take(CommitBatchSize).ToDataTable();
                        BulkInsert(dt);
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public void BulkInsert(DataTable dt)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    SqlBulkCopy bulkCopy = new SqlBulkCopy(connection,
                        SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.UseInternalTransaction,
                        null)
                    {
                        DestinationTableName = TableName
                    };

                    foreach (DataColumn col in dt.Columns) // 데이터 도착 테이블의 컬럼명과 내 DataTable의 컬럼명을 매칭시킴.
                    {
                        bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                    }

                    connection.Open();
                    bulkCopy.WriteToServer(dt); // WriteToServer는 DataTable 또는 DataRow[] 파라미터 형태로만 가능하다.
                    connection.Close();
                }
            }
            catch
            {
                throw;
            }
        }
    }

    public static class BulkUploadToSqlHelper
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> data)
        {
            try
            {
                PropertyInfo[] properties = typeof(T).GetProperties();
                var table = new DataTable();

                foreach (PropertyInfo prop in properties)
                {
                    if (!prop.GetGetMethod().IsVirtual) // Entity Framework에서 관계가 있는 Table끼리는 virtual ICollection으로 객체가 생성되는데 무시하기 위함.
                    {
                        table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                    }
                }

                foreach (T item in data)
                {
                    DataRow row = table.NewRow();

                    foreach (PropertyInfo prop in properties)
                    {
                        if (!prop.GetGetMethod().IsVirtual) // Entity Framework에서 관계가 있는 Table끼리는 virtual ICollection으로 객체가 생성되는데 무시하기 위함.
                        {
                            row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                        }
                    }

                    table.Rows.Add(row);
                }

                return table;
            }
            catch
            {
                throw;
            }
        }
    }
}
