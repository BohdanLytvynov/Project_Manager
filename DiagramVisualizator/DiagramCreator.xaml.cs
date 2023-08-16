using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.ObjectModel;


namespace DiagramVisualizator
{
    /// <summary>
    /// Логика взаимодействия для DiagramCreator.xaml
    /// </summary>
    public partial class DiagramCreator : UserControl
    {
        #region Fields

        private Action CallDrawDiagram; //Delegate that points at the method which draw diagram

        public static readonly DependencyProperty DateTimeCollectionProperty; //Argument x

        public static readonly DependencyProperty ObjectCollectionProperty; //Argument y

        public static readonly DependencyProperty WidthActualProperty; //ActualWindowWidth

        public static readonly DependencyProperty HeightActualProperty; //ActualWindowHeight
        
        #endregion

        #region Properties        

        public double HeightActual //Property that will be used while markup
        {
            get { return (double)GetValue(HeightActualProperty); }
            set { SetValue(HeightActualProperty, value); }
        }
                
        public double WidthActual //Property that will be used while markup
        {
            get { return (double)GetValue(WidthActualProperty); }
            set { SetValue(WidthActualProperty, value); }
        }
                            
        public ObservableCollection<DateTime> DateTimeCollection //Property that will be used while markup
        {
            get { return (ObservableCollection<DateTime>)GetValue(DateTimeCollectionProperty); }
            set { SetValue(DateTimeCollectionProperty, value); }
        }

        public ObservableCollection<double> ObjectCollection //Property that will be used while markup
        {
            get { return (ObservableCollection<double>)GetValue(ObjectCollectionProperty); }
            set { SetValue(ObjectCollectionProperty, value); }
        }

        #endregion

        #region Constructor

        public DiagramCreator()
        {
            InitializeComponent();

            CallDrawDiagram = DrawDiagram;
        }
        /// <summary>
        /// Register Dependdency properties
        /// </summary>
        static DiagramCreator()
        {
            DateTimeCollectionProperty =
           DependencyProperty.Register("DateTimeCollection", 
           typeof(ObservableCollection<DateTime>), 
           typeof(DiagramCreator), new PropertyMetadata(null
           , OnDateTimeCollectionChanged));

            ObjectCollectionProperty =
           DependencyProperty.Register("ObjectCollection", 
           typeof(ObservableCollection<double>), 
           typeof(DiagramCreator), new PropertyMetadata(null));

            WidthActualProperty = DependencyProperty.Register("WidthActual",
                typeof(double),
                typeof(DiagramCreator), new PropertyMetadata(0.0
                ));

            HeightActualProperty =
           DependencyProperty.Register("HeightActual", typeof(double),
               typeof(DiagramCreator), new PropertyMetadata(0.0, OnActualHeightChanged));
        }

        #endregion

        #region Methods
        /// <summary>
        /// Draws Diagram
        /// </summary>
        public void DrawDiagram()
        {
            this.DiagramStack.Children.Clear();
                        
            for (int i = 0; i < DateTimeCollection.Count; i++)
            {
                if (DateTimeCollection?.Count == 0 || ObjectCollection?.Count == 0
                    || DateTimeCollection?.Count != ObjectCollection?.Count)
                {
                    return;
                }

                var stack = new StackPanel() { Margin = new Thickness(10,0,10,0)};
                stack.Orientation = Orientation.Vertical;
                stack.VerticalAlignment = VerticalAlignment.Bottom;

                stack.Children.Add(
                    new TextBlock() //1st up argument
                    {
                        Text = $"{ObjectCollection[i]} ед.",
                        Width = 60,
                        Foreground = Brushes.Black,
                        FontSize = 14,
                        Margin = new Thickness(0, 3, 0, 3)
                    });


                stack.Children.Add(
                    new Rectangle() //Rectangle
                    {
                        Width = 20,
                        Height = SetSizeOfRectangle(ObjectCollection[i]),
                        Fill = Brushes.Red,
                        Margin = new Thickness(0, 3, 0, 3)
                    }) ;

                stack.Children.Add(
                    new Rectangle() //separator
                    {Width = 50, Fill = Brushes.Black, Height=3 }
                    );

                stack.Children.Add(
                    new TextBlock()//\date argument
                    {
                        Text = $"{DateTimeCollection[i].Month}." +
                        $"{DateTimeCollection[i].Year}",
                        Width = 50,
                        Foreground = Brushes.Black,
                        FontSize = 14,
                        Margin = new Thickness(0, 3, 0, 3)
                    }) ;

                DiagramStack.Children.Add(stack);

                Calculate();
            }            
        }
        /// <summary>
        /// Calculate and show all function's main characteristics
        /// </summary>
        private void Calculate()
        {
            var min = ObjectCollection.Min<double>();

            var max = ObjectCollection.Max<double>();

            var minindex = ObjectCollection.IndexOf(min);

            var maxindex = ObjectCollection.IndexOf(max);

            maxValue.Text = max.ToString();

            minValue.Text = min.ToString();

            maxDate.Text = FormDateTimeString(DateTimeCollection[maxindex]);

            minDate.Text = FormDateTimeString(DateTimeCollection[minindex]);
        }
        /// <summary>
        /// Forms a string from dateTime, which includes the month and the year
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private string FormDateTimeString(DateTime date)
        { 
            return $"{date.Month}.{date.Year}";
        }
        /// <summary>
        /// will fire when actualheight of the window will be changed
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private static void OnActualHeightChanged(DependencyObject o,
            DependencyPropertyChangedEventArgs e)
        {
            var This = (DiagramCreator)o;

            This?.CallDrawDiagram.Invoke();
        }
        /// <summary>
        /// will fire when DateTime Collection Will Be Changed
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private static void OnDateTimeCollectionChanged(DependencyObject o, 
            DependencyPropertyChangedEventArgs e)
        {
            if ((e.NewValue as ObservableCollection<DateTime>)?.Count == 0
                || e.NewValue == null)
                return;

            var This = (DiagramCreator)o;

            if (e.OldValue != e.NewValue)
            {
                This.CallDrawDiagram.Invoke();
            }
            
        }
        /// <summary>
        /// Sets the size of rectangle according to window height
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private double SetSizeOfRectangle(double value)
        {
            string str = value.ToString();

            var newnumber = "1";
            for (int i = 0; i < str.Length - 2; i++)
            {
                newnumber += "0";
            }

            return (value / double.Parse(newnumber)) + (HeightActual/6);
        }

        #endregion
    }

}
