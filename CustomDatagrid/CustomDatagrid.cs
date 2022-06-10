using System.Reflection;

namespace CustomDatagrid
{
    public partial class CustomDatagrid : Form
    {
        public int Page { get; set; } = 1;
        public int ItemsPerPage { get; set; } = 10;
        public List<Person> People { get; set; } = new();
        public int TotalPages { get; set; } = 1;
        public string Search { get; set; } = "";
        public string Sort { get; set; } = "Id";
        public string SortDirection { get; set; } = "ASC";
        public int SelectedColumn = -1;
        public double Responsive { get; set; } = 0;
        public double ResponsiveHeight { get; set; } = 0;
        public PropertyInfo[] PropertyInfos;
        public Point Pointer = new();
        public Font font = new("Arial", 15);
        public void SetPeople() { People = API.GetPeople(Page, ItemsPerPage, Search, Sort, SortDirection); }
        public void SetTotalPages() { TotalPages = API.GetTotalPages(ItemsPerPage, Search); }
        public List<Rectangle> pageButtons = new();
        public List<int> shownPages = new();

        public List<int> MaxWidths = new();
        public List<List<Rectangle>> rowRectangles = new();
        public List<Rectangle> headerRectangles = new();

        public List<int> selectedRows = new();
        public CustomDatagrid()
        {
            InitializeComponent();
            PropertyInfos = typeof(Person).GetProperties();
            RefreshData();
            ComboBox_ItemsPerPage.SelectedIndex = ComboBox_ItemsPerPage.FindStringExact(ItemsPerPage.ToString());
        }
        private void Panel_Paint(object sender, PaintEventArgs e) { SetMaxWidths(); GenerateTable(); DrawTable(Panel.CreateGraphics()); }

        private void ComboBox_ItemsPerPage_SelectedValueChanged(object sender, EventArgs e)
        {
            ItemsPerPage = int.Parse(ComboBox_ItemsPerPage.Text);
            Page = 1;
            RefreshData();
        }

        private void TextBox_Search_TextChanged(object sender, EventArgs e)
        {
            Search = TextBox_Search.Text;
            Page = 1;
            RefreshData();
        }

        public void RefreshData()
        {
            SetPeople();
            SetTotalPages();
            SetMaxWidths();
            GenerateTable();
            CreatePageButtons();
            DrawTable(Panel.CreateGraphics());
        }

        public void CreatePageButtons()
        {
            pageButtons.Clear();
            shownPages.Clear();
            pageButtons.Add(new Rectangle(Panel.Width - 90, Panel.Height - 30, 25, 25));
            pageButtons.Add(new Rectangle(Panel.Width - 60, Panel.Height - 30, 25, 25));
            pageButtons.Add(new Rectangle(Panel.Width - 30, Panel.Height - 30, 25, 25));
            if (Page is 1 or 2)
            {
                shownPages.Add(1); shownPages.Add(2); shownPages.Add(3);
            }
            else if (Page == TotalPages)
            {
                shownPages.Add(Page - 2); shownPages.Add(Page - 1); shownPages.Add(Page);
            }
            else
            {
                shownPages.Add(Page - 1); shownPages.Add(Page); shownPages.Add(Page + 1);
            }
        }

        public void SetMaxWidths()
        {
            if (People.Count == 0)
            {
                MaxWidths.Clear();
                foreach (PropertyInfo property in PropertyInfos)
                {
                    MaxWidths.Add(0);
                }
                return;
            }
            MaxWidths.Clear();
            foreach (PropertyInfo property in PropertyInfos)
            {
                MaxWidths.Add(People.Max(x => x.GetType().GetProperty(property.Name).GetValue(x).ToString().Length) + 3);
            }
            Responsive = (double)(Panel.Width - (double)50) / (MaxWidths.Sum(x => x) * (double)10);
            ResponsiveHeight = (double)(Panel.Height - (double)180) / ItemsPerPage;
        }

        public void GenerateTable()
        {
            rowRectangles.Clear();
            headerRectangles.Clear();
            int columnOffset = 0;
            for (int i = 0; i < PropertyInfos.Length; i++)
            {

                List<Rectangle> rectangles = new();
                for (int j = 0; j <= People.Count; j++)
                {
                    if (j == 0)
                    {
                        headerRectangles.Add(new Rectangle() { X = 30 + columnOffset, Y = 50 + ((int)Math.Ceiling(ResponsiveHeight) * j), Height = (int)Math.Ceiling(ResponsiveHeight), Width = (int)Math.Ceiling(10 * MaxWidths[i] * Responsive) });
                        continue;
                    }
                    rectangles.Add(new Rectangle() { X = 30 + columnOffset, Y = 50 + ((int)Math.Ceiling(ResponsiveHeight) * j), Height = (int)Math.Ceiling(ResponsiveHeight), Width = (int)Math.Ceiling(10 * MaxWidths[i] * Responsive) });
                }
                columnOffset += (int)Math.Ceiling(10 * MaxWidths[i] * Responsive);
                rowRectangles.Add(rectangles);
            }
        }

        public void DrawTable(Graphics g)
        {
            CreatePageButtons();
            g.Clear(Color.White);
            g.DrawString("Page " + Page + " of " + TotalPages, font, Brushes.Black, Panel.Width - 130, Panel.Height - 60);
            g.DrawString("Items per page: ", font, Brushes.Black, 10, 0);
            g.DrawString("Search: ", font, Brushes.Black, Panel.Width - 190, 0);

            #region PageButtons
            for (int i = 0; i < pageButtons.Count; i++)
            {
                g.FillRectangle(shownPages[i] == Page ? Brushes.DarkGray : Brushes.LightGray, pageButtons[i]);
                TextRenderer.DrawText(g, shownPages[i].ToString(), font, pageButtons[i], Color.Black);
            }
            #endregion

            foreach (Rectangle rectangle in headerRectangles)
            {
                g.FillRectangle(headerRectangles.IndexOf(rectangle) == SelectedColumn ? Brushes.LightBlue : Brushes.Gray, rectangle);
                g.DrawRectangle(Pens.Black, rectangle);
                TextRenderer.DrawText(g, PropertyInfos[headerRectangles.IndexOf(rectangle)].Name, font, rectangle, Color.Black);
            }

            if (SelectedColumn != -1)
            {
                Point[] pointer = new Point[] { new Point(Pointer.X, Pointer.Y), new Point(Pointer.X - 7, Pointer.Y - 15), new Point(Pointer.X + 7, Pointer.Y - 15) };
                g.FillPolygon(Brushes.Black, pointer);
            }

            foreach (List<Rectangle> rectangles in rowRectangles)
            {
                foreach (Rectangle rectangle in rectangles)
                {
                    g.FillRectangle(selectedRows.Contains(int.Parse(typeof(Person).GetProperty("Id").GetValue(People[rectangles.IndexOf(rectangle)], null).ToString())) ? new SolidBrush(Color.FromArgb(150, 0, 0, 0)) : Brushes.LightGray, rectangle);
                    g.DrawRectangle(Pens.Black, rectangle);
                    TextRenderer.DrawText(g, PropertyInfos[rowRectangles.IndexOf(rectangles)].GetValue(People[rectangles.IndexOf(rectangle)], null).ToString().Split("0:00:00")[0], font, rectangle, Color.Black);
                }
            }

        }

        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                foreach (Rectangle rectangle in headerRectangles)
                {
                    if (rectangle.Contains(e.Location))
                    {
                        SelectedColumn = headerRectangles.IndexOf(rectangle);
                        RefreshData();
                        return;
                    }
                }
            }

            if (e.Button == MouseButtons.Left)
            {
                #region PageButtons
                foreach (Rectangle rectangle in pageButtons.ToList())
                {
                    if (rectangle.Contains(e.Location))
                    {
                        switch (pageButtons.IndexOf(rectangle))
                        {
                            case 0:
                                if (Page > 1)
                                {
                                    Page--;
                                }
                                break;
                            case 1:
                                if (Page == 1 && Page != TotalPages)
                                {
                                    Page++;
                                    break;
                                }
                                else if (Page == TotalPages && Page != 1)
                                {
                                    Page--;
                                }
                                break;
                            case 2:
                                if (Page < TotalPages)
                                {
                                    Page++;
                                }
                                break;
                        }
                        RefreshData();
                        return;
                    }
                }
                #endregion

                foreach (Rectangle rectangle in headerRectangles)
                {
                    if (rectangle.Contains(e.Location))
                    {
                        string sortNow = PropertyInfos[headerRectangles.IndexOf(rectangle)].Name;
                        SortDirection = sortNow == Sort ? SortDirection == "ASC" ? "DSC" : "ASC" : "ASC";
                        Sort = sortNow;
                        RefreshData();
                        return;
                    }
                }

                foreach (List<Rectangle> rectangles in rowRectangles)
                {
                    foreach (Rectangle rectangle in rectangles)
                    {
                        if (rectangle.Contains(e.Location))
                        {
                            int id = int.Parse(typeof(Person).GetProperty("Id").GetValue(People[rectangles.IndexOf(rectangle)], null).ToString());
                            if (selectedRows.Contains(id))
                            {
                                _ = selectedRows.Remove(id);
                            }
                            else
                            {
                                selectedRows.Add(id);
                            }
                            RefreshData();
                            return;
                        }
                    }
                }
            }
        }

        private void Panel_MouseUp(object sender, MouseEventArgs e)
        {
            if (SelectedColumn != -1)
            {
                List<RectangleDistance> rectangleDistance = new();
                foreach (Rectangle rectangle in headerRectangles)
                {
                    double dx = e.X - (rectangle.X + (rectangle.Width / 2));
                    double dy = e.Y - (rectangle.Y + (rectangle.Height / 2));
                    rectangleDistance.Add(new RectangleDistance() { Distance = (dx * dx) + (dy * dy), Rectangle = rectangle, Index = headerRectangles.IndexOf(rectangle) });
                }
                rectangleDistance = rectangleDistance.OrderBy(x => x.Distance).Take(3).ToList();
                if (!(SelectedColumn == 0 && headerRectangles.IndexOf(rectangleDistance[0].Rectangle) == 0) && !(SelectedColumn == headerRectangles.Count - 1 && headerRectangles.IndexOf(rectangleDistance[0].Rectangle) == SelectedColumn) && !(SelectedColumn == headerRectangles.IndexOf(rectangleDistance[0].Rectangle)))
                {
                    List<RectangleDistance> rd = rectangleDistance.Where(x => x.Index != SelectedColumn).OrderBy(x => x.Index).ToList();
                    PropertyInfo selectedInfo = PropertyInfos[SelectedColumn];
                    List<PropertyInfo> infos = PropertyInfos.ToList();
                    infos.Insert(rd[1].Index, selectedInfo);
                    if (rd[1].Index < SelectedColumn)
                    {
                        infos.RemoveAt(SelectedColumn + 1);
                    }
                    else
                    {
                        infos.RemoveAt(SelectedColumn);
                    }

                    PropertyInfos = infos.ToArray();
                }
                SelectedColumn = -1;
                RefreshData();
            }
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (SelectedColumn != -1)
            {
                List<RectangleDistance> rectangleDistance = new();
                foreach (Rectangle rectangle in headerRectangles)
                {
                    double dx = e.X - (rectangle.X + (rectangle.Width / 2));
                    double dy = e.Y - (rectangle.Y + (rectangle.Height / 2));
                    rectangleDistance.Add(new RectangleDistance() { Distance = (dx * dx) + (dy * dy), Rectangle = rectangle, Index = headerRectangles.IndexOf(rectangle) });
                }
                rectangleDistance = rectangleDistance.OrderBy(x => x.Distance).Take(3).Where(x => x.Index != SelectedColumn).OrderBy(x => x.Index).ToList();
                Pointer = rectangleDistance[1].Rectangle.Location;
                DrawTable(Panel.CreateGraphics());
            }
        }
    }
    public class RectangleDistance
    {
        public double Distance { get; set; }
        public Rectangle Rectangle { get; set; }
        public int Index { get; set; }
    }
}