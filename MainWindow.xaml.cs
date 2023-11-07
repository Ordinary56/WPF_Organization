using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Organization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Organization> organizations = new ObservableCollection<Organization>();
        public ICollectionView OrganizationCollection { get; }
        
        void LoadOrganizations()
        {

            using (StreamReader sr = new(@".\organizations-100000.csv"))
            {
                sr.ReadLine()!.Skip(1);
                while (!sr.EndOfStream)
                {
                    organizations.Add(new Organization(sr.ReadLine()!.Split(';')));
                }
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            LoadOrganizations();
            this.DataContext = this;
            OrganizationCollection = CollectionViewSource.GetDefaultView(organizations);
            LoadCBItems();

        }

        private bool FilterOrgs(object obj)
        {
            Organization org = obj as Organization;
            if (!string.IsNullOrEmpty((string)cb_Country.SelectedItem) && cb_Year.SelectedItem != null)
            {
                return org.Country == (string)cb_Country.SelectedItem && org.Founded == (int)cb_Year.SelectedItem;
            }
            else if (string.IsNullOrEmpty((string)cb_Country.SelectedItem))
            {
                return org.Founded == (int)cb_Year.SelectedIndex;
            }
            else if (cb_Year.SelectedItem == null)
            {
                return org.Country == (string)cb_Country.SelectedItem;
            }
            else
            {
                return true;
            }



        }

        void LoadCBItems()
        {
            organizations.GroupBy(x => x.Country).DistinctBy(x => x.Key).ToList().ForEach(x => cb_Country.Items.Add(x.Key));
            organizations.GroupBy(x => x.Founded).DistinctBy(x => x.Key).ToList().ForEach(x => cb_Year.Items.Add(x.Key));
            cb_Country.Items.Add("-");
            cb_Year.Items.Add("-");
        }

        private void cb_Year_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OrganizationCollection.Filter += FilterOrgs;
        }

        private void cb_Country_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OrganizationCollection.Filter += FilterOrgs;
        }
    }
}
