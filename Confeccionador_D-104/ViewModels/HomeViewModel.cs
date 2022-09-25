using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Confeccionador_D_104.Helpers;

namespace Confeccionador_D_104.ViewModels
{
    internal class HomeViewModel : ObservableRecipient
    {

        #region ViewConfigVariables

        private readonly int maxItemsForYearComboBox = 2;

        private List<string> monthList;

        public List<string> MonthList
        {
            get { return monthList; }
            set { SetProperty(ref monthList, value); }
        }

        private List<int> yearList;

        public List<int> YearList
        {
            get { return yearList; }
            set { SetProperty(ref yearList, value); }
        }

        #endregion


        #region SelectionVariables

        private string folderPath;

        public string FolderPath
        {

            get { return folderPath; }
            set { SetProperty(ref folderPath, value); }
        }

        private string personIdentification;

        public string PersonIdentification
        {
            get { return personIdentification; }
            set { SetProperty(ref personIdentification, value); }
        }

        private string selectedMonth;
        public string SelectedMonth
        {
            get { return selectedMonth; }
            set { SetProperty(ref selectedMonth, value); }
        }

        private string selectedYear;
        public string SelectedYear
        {
            get { return selectedYear; }
            set { SetProperty(ref selectedYear, value); }
        }

        #endregion

        #region RelayCommands

        public RelayCommand LoadInvoicesCmd { get; set;}

        public RelayCommand SelectPathCmd { get; set;}
        #endregion

        #region Constructor

        public HomeViewModel()
        {

            //Need Months in spanish
            //var months = System.Globalization.DateTimeFormatInfo.InvariantInfo.MonthNames;

            monthList = GlobalData.MONTHS.ToList();

            DateTime today = DateTime.Now;

            yearList = new List<int>();

            for (int i = 0; i < maxItemsForYearComboBox; i++)
            {
                yearList.Add(today.Year - i);
            }

            /*
             * ###############################
             * Relay Commands initialization
             * ###############################
             */

            LoadInvoicesCmd = new RelayCommand(LoadInvoices);
            SelectPathCmd = new RelayCommand(SelectPath);

        }

        #endregion

        

        #region Methods

        private void LoadInvoices()
        {
            try
            {
                //DEVELOPER DEBUGIN VARIABLES DATA
                folderPath = "C:\\Users\\jason\\Downloads\\testing";
                personIdentification = "303140635";
                selectedMonth = "Enero";
                selectedYear = "2022";

                if (!String.IsNullOrEmpty(selectedMonth) && !String.IsNullOrEmpty(selectedYear)
                    && !String.IsNullOrEmpty(personIdentification) && !String.IsNullOrEmpty(folderPath))
                {

                    int indexOfSelectedMonth = monthList.FindIndex(m => m == selectedMonth);
                    int yearIntParsed = Int32.Parse(SelectedYear);

                    //XmlReader is my own invoice xmlReader class
                    XmlReader reader = new XmlReader(PersonIdentification, indexOfSelectedMonth + 1, yearIntParsed);

                    reader.FindInvoices(new DirectoryInfo(folderPath));
                }
                else
                {
                    MessageBox.Show("Por favor seleccione/rellene todos los datos necesarios antes de continuar",
                        "Error Datos Faltantes", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void SelectPath()
        {

            using (var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    FolderPath = folderBrowserDialog.SelectedPath;
                }
            }
        }


        #endregion


    }

}
