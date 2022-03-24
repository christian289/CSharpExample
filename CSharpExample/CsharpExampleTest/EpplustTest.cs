using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CsharpExampleTest
{
    // Epplus는 OpenXml 기반으로 되어있는 .NET 스프레드시트 라이브러리 중 가장 유명한 라이브러리 입니다.
    // 다만 4.5.3.3 버전을 마지막으로 무료 상업버전은 종료되었고, Open 소스 관리는 되지 않습니다.
    // Version 5 이상부터는 상업용으로 사용할 경우 반드시 구매를 해야합니다. https://epplussoftware.com/docs/5.0/articles/readme.html
    // Epplus가 지원이 되지 않아 사용하기 꺼려진다면 ClosedXml 이라는 라이브러리도 있습니다.
    // 다만 ClosedXml은 메모리가 상당히 많이 소요된다는 이슈가 존재합니다.
    public class EpplustTest
    {
        private const int baseRowIdx = 1;
        private const int baseColIdx = 1;

        private DataTable MakeSampleDataTable1()
        {
            string columnName1 = "라비앙로즈";
            string columnName2 = "비올레타";
            string columnName3 = "피에스타";
            string columnName4 = "파노라마";

            var dt = new DataTable();
            dt.Columns.Add(new DataColumn(columnName1, typeof(string)));
            dt.Columns.Add(new DataColumn(columnName2, typeof(string)));
            dt.Columns.Add(new DataColumn(columnName3, typeof(string)));
            dt.Columns.Add(new DataColumn(columnName4, typeof(string)));

            DataRow dr;
            dr = dt.NewRow();
            dr[columnName1] = "장원영";
            dr[columnName2] = "안유진";
            dr[columnName3] = "조유리";
            dr[columnName4] = "권은비";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[columnName1] = "최예나";
            dr[columnName2] = "강혜원";
            dr[columnName3] = "야부키나코";
            dr[columnName4] = "미와야키사쿠라";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[columnName1] = "혼다히토미";
            dr[columnName2] = "이채연";
            dr[columnName3] = "김채원";
            dr[columnName4] = "김민주";
            dt.Rows.Add(dr);

            return dt;
        }

        private DataTable MakeSampleDataTable2()
        {
            string columnName1 = "에어플레인";
            string columnName2 = "하이라이트";
            string columnName3 = "우연이아니야";
            string columnName4 = "느린여행";

            var dt = new DataTable();
            dt.Columns.Add(new DataColumn(columnName1, typeof(string)));
            dt.Columns.Add(new DataColumn(columnName2, typeof(string)));
            dt.Columns.Add(new DataColumn(columnName3, typeof(string)));
            dt.Columns.Add(new DataColumn(columnName4, typeof(string)));

            DataRow dr;
            dr = dt.NewRow();
            dr[columnName1] = "장원영\r\n최예나\r\n혼다히토미";
            dr[columnName2] = "안유진\r\n강혜원\r\n이채연";
            dr[columnName3] = "조유리\r\n야부키나코\r\n김채원";
            dr[columnName4] = "권은비\r\n미와야키사쿠라\r\n김민주";
            dt.Rows.Add(dr);

            return dt;
        }

        [Theory]
        [InlineData("엑셀파일경로입력")]
        public void ExcelSave1(string filePath)
        {
            DataTable dt = MakeSampleDataTable1();

            string name = filePath;
            using ExcelPackage pack = new(new FileInfo(name));
            ExcelWorksheet ws = pack.Workbook.Worksheets.Add("주문목록");
            ws.Cells["A1"].LoadFromDataTable(dt, true);
            pack.Save();
        }

        [Theory]
        [InlineData("엑셀파일경로입력")]
        public void ExcelSave2(string filePath)
        {
            DataTable dt = MakeSampleDataTable2();
            string name = filePath;
            using var pack = new ExcelPackage(new FileInfo(name));
            ExcelWorksheet ws = pack.Workbook.Worksheets.Add("주문목록");
            ws.Cells["A1"].LoadFromDataTable(dt, true);
            pack.Save();
        }

        [Theory]
        [InlineData("엑셀파일경로입력")]
        public void ExcelColumnSizeAutoFit(string filePath)
        {
            string name = filePath;
            using var pack = new ExcelPackage(new FileInfo(name));
            ExcelWorksheet ws = pack.Workbook.Worksheets[1]; // 첫번째 워크시트
            ws.Column(1).AutoFit();
            ws.Column(2).AutoFit();
            ws.Column(3).AutoFit();
            ws.Column(4).AutoFit();
            pack.Save();
        }

        [Theory]
        [InlineData("엑셀파일경로입력")]
        public void ExcelRowHeightChange(string filePath)
        {
            string name = filePath;
            using var pack = new ExcelPackage(new FileInfo(name));
            ExcelWorksheet ws = pack.Workbook.Worksheets[1]; // 첫번째 워크시트
                                                             //ws.Row(1).CustomHeight = true; // 이 옵션없이도 Row.Height 변경 가능
            ws.Row(1).Height = 50;
            ws.Row(2).Height = 30;
            ws.Row(3).Height = 70;
            ws.Row(4).Height = 100;
            pack.Save();
        }

        [Theory]
        [InlineData("엑셀파일경로입력")]
        public void ExcelGetAddress(string filePath)
        {
            using var pack = new ExcelPackage(new FileInfo(filePath));
            ExcelWorksheet ws = pack.Workbook.Worksheets[1]; // 첫번째 워크시트
            string asdf = ws.Dimension.Address;
            string asdfzxcv = ws.Dimension.End.Address;
        }
    }
}
