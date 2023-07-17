using System.Collections.Generic;
using System.Windows;

namespace WpfCornerRoundedDataGrid
{
    /// <summary>
    /// https://stackoverflow.com/questions/8120205/datagrid-template-with-rounded-corners
    /// 참고
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //List<Person> people = new()
            //{
            //    new Person
            //    {
            //        Name = "John1",
            //        Age1 = 2500000000000000,
            //        Age2 = 2500000000000000,
            //        Age3 = 2500000000000000,
            //        Age4 = 2500000000000000,
            //        Age5 = 2500000000000000,
            //        Age6 = 2500000000000000,
            //        Age7 = 2500000000000000,
            //        Age8 = 2500000000000000,
            //        Age9 = 2500000000000000,
            //    },
            //    new Person
            //    {
            //        Name = "John1",
            //        Age1 = 2500000000000000,
            //        Age2 = 2500000000000000,
            //        Age3 = 2500000000000000,
            //        Age4 = 2500000000000000,
            //        Age5 = 2500000000000000,
            //        Age6 = 2500000000000000,
            //        Age7 = 2500000000000000,
            //        Age8 = 2500000000000000,
            //        Age9 = 2500000000000000,
            //    },
            //    new Person
            //    {
            //        Name = "John1",
            //        Age1 = 2500000000000000,
            //        Age2 = 2500000000000000,
            //        Age3 = 2500000000000000,
            //        Age4 = 2500000000000000,
            //        Age5 = 2500000000000000,
            //        Age6 = 2500000000000000,
            //        Age7 = 2500000000000000,
            //        Age8 = 2500000000000000,
            //        Age9 = 2500000000000000,
            //    },
            //    new Person
            //    {
            //        Name = "John1",
            //        Age1 = 2500000000000000,
            //        Age2 = 2500000000000000,
            //        Age3 = 2500000000000000,
            //        Age4 = 2500000000000000,
            //        Age5 = 2500000000000000,
            //        Age6 = 2500000000000000,
            //        Age7 = 2500000000000000,
            //        Age8 = 2500000000000000,
            //        Age9 = 2500000000000000,
            //    },
            //    new Person
            //    {
            //        Name = "John1",
            //        Age1 = 2500000000000000,
            //        Age2 = 2500000000000000,
            //        Age3 = 2500000000000000,
            //        Age4 = 2500000000000000,
            //        Age5 = 2500000000000000,
            //        Age6 = 2500000000000000,
            //        Age7 = 2500000000000000,
            //        Age8 = 2500000000000000,
            //        Age9 = 2500000000000000,
            //    },
            //    new Person
            //    {
            //        Name = "John1",
            //        Age1 = 2500000000000000,
            //        Age2 = 2500000000000000,
            //        Age3 = 2500000000000000,
            //        Age4 = 2500000000000000,
            //        Age5 = 2500000000000000,
            //        Age6 = 2500000000000000,
            //        Age7 = 2500000000000000,
            //        Age8 = 2500000000000000,
            //        Age9 = 2500000000000000,
            //    },
            //    new Person
            //    {
            //        Name = "John1",
            //        Age1 = 2500000000000000,
            //        Age2 = 2500000000000000,
            //        Age3 = 2500000000000000,
            //        Age4 = 2500000000000000,
            //        Age5 = 2500000000000000,
            //        Age6 = 2500000000000000,
            //        Age7 = 2500000000000000,
            //        Age8 = 2500000000000000,
            //        Age9 = 2500000000000000,
            //    },
            //    new Person
            //    {
            //        Name = "John1",
            //        Age1 = 2500000000000000,
            //        Age2 = 2500000000000000,
            //        Age3 = 2500000000000000,
            //        Age4 = 2500000000000000,
            //        Age5 = 2500000000000000,
            //        Age6 = 2500000000000000,
            //        Age7 = 2500000000000000,
            //        Age8 = 2500000000000000,
            //        Age9 = 2500000000000000,
            //    },
            //    new Person
            //    {
            //        Name = "John1",
            //        Age1 = 2500000000000000,
            //        Age2 = 2500000000000000,
            //        Age3 = 2500000000000000,
            //        Age4 = 2500000000000000,
            //        Age5 = 2500000000000000,
            //        Age6 = 2500000000000000,
            //        Age7 = 2500000000000000,
            //        Age8 = 2500000000000000,
            //        Age9 = 2500000000000000,
            //    },
            //    new Person
            //    {
            //        Name = "John1",
            //        Age1 = 2500000000000000,
            //        Age2 = 2500000000000000,
            //        Age3 = 2500000000000000,
            //        Age4 = 2500000000000000,
            //        Age5 = 2500000000000000,
            //        Age6 = 2500000000000000,
            //        Age7 = 2500000000000000,
            //        Age8 = 2500000000000000,
            //        Age9 = 2500000000000000,
            //    },
            //    new Person
            //    {
            //        Name = "John1",
            //        Age1 = 2500000000000000,
            //        Age2 = 2500000000000000,
            //        Age3 = 2500000000000000,
            //        Age4 = 2500000000000000,
            //        Age5 = 2500000000000000,
            //        Age6 = 2500000000000000,
            //        Age7 = 2500000000000000,
            //        Age8 = 2500000000000000,
            //        Age9 = 2500000000000000,
            //    },
            //    new Person
            //    {
            //        Name = "John1",
            //        Age1 = 2500000000000000,
            //        Age2 = 2500000000000000,
            //        Age3 = 2500000000000000,
            //        Age4 = 2500000000000000,
            //        Age5 = 2500000000000000,
            //        Age6 = 2500000000000000,
            //        Age7 = 2500000000000000,
            //        Age8 = 2500000000000000,
            //        Age9 = 2500000000000000,
            //    },
            //    new Person
            //    {
            //        Name = "John1",
            //        Age1 = 2500000000000000,
            //        Age2 = 2500000000000000,
            //        Age3 = 2500000000000000,
            //        Age4 = 2500000000000000,
            //        Age5 = 2500000000000000,
            //        Age6 = 2500000000000000,
            //        Age7 = 2500000000000000,
            //        Age8 = 2500000000000000,
            //        Age9 = 2500000000000000,
            //    },
            //    new Person
            //    {
            //        Name = "John1",
            //        Age1 = 2500000000000000,
            //        Age2 = 2500000000000000,
            //        Age3 = 2500000000000000,
            //        Age4 = 2500000000000000,
            //        Age5 = 2500000000000000,
            //        Age6 = 2500000000000000,
            //        Age7 = 2500000000000000,
            //        Age8 = 2500000000000000,
            //        Age9 = 2500000000000000,
            //    },
            //    new Person
            //    {
            //        Name = "John1",
            //        Age1 = 2500000000000000,
            //        Age2 = 2500000000000000,
            //        Age3 = 2500000000000000,
            //        Age4 = 2500000000000000,
            //        Age5 = 2500000000000000,
            //        Age6 = 2500000000000000,
            //        Age7 = 2500000000000000,
            //        Age8 = 2500000000000000,
            //        Age9 = 2500000000000000,
            //    },
            //};

            List<Person> people = new()
            {
                new Person
                {
                    Name = "John1",
                    Age1 = 25000,
                    Age2 = 25000,
                    Age3 = 25000,
                    Age4 = 25000,
                    Age5 = 25000,
                    Age6 = 25000,
                    Age7 = 25000,
                    Age8 = 25000,
                    Age9 = 25000,
                },
                new Person
                {
                    Name = "John1",
                    Age1 = 25000,
                    Age2 = 25000,
                    Age3 = 25000,
                    Age4 = 25000,
                    Age5 = 25000,
                    Age6 = 25000,
                    Age7 = 25000,
                    Age8 = 25000,
                    Age9 = 25000,
                },
                new Person
                {
                    Name = "John1",
                    Age1 = 25000,
                    Age2 = 25000,
                    Age3 = 25000,
                    Age4 = 25000,
                    Age5 = 25000,
                    Age6 = 25000,
                    Age7 = 25000,
                    Age8 = 25000,
                    Age9 = 25000,
                },
                new Person
                {
                    Name = "John1",
                    Age1 = 25000,
                    Age2 = 25000,
                    Age3 = 25000,
                    Age4 = 25000,
                    Age5 = 25000,
                    Age6 = 25000,
                    Age7 = 25000,
                    Age8 = 25000,
                    Age9 = 25000,
                },
                new Person
                {
                    Name = "John1",
                    Age1 = 25000,
                    Age2 = 25000,
                    Age3 = 25000,
                    Age4 = 25000,
                    Age5 = 25000,
                    Age6 = 25000,
                    Age7 = 25000,
                    Age8 = 25000,
                    Age9 = 25000,
                },
            };

            myDataGrid.ItemsSource = people;
            myDataGrid2.ItemsSource = people;
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public long Age1 { get; set; }
        public long Age2 { get; set; }
        public long Age3 { get; set; }
        public long Age4 { get; set; }
        public long Age5 { get; set; }
        public long Age6 { get; set; }
        public long Age7 { get; set; }
        public long Age8 { get; set; }
        public long Age9 { get; set; }
    }

    public class FreezableBindingProxy : Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            return new FreezableBindingProxy();
        }

        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object), typeof(FreezableBindingProxy));
    }
}
