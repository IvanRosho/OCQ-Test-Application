using CC.Mvvm;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace OCQ_Test_Application.ViewModels
{
    public class PersonViewModel : CC.Mvvm.BaseViewModel
    {
        #region private fields
        private Models.OCQSofttestContext db;
        private Models.Person selectedPerson;
        #endregion
        #region Commands
        public DelegateCommand SaveCommand => saveCommand ??= new DelegateCommand(SaveMethod);
        public DelegateCommand NewCommand => newCommand ??= new DelegateCommand(NewMethod);

        private DelegateCommand? saveCommand;
        private DelegateCommand? newCommand;
        #endregion
        #region public properties
        public bool IsLoaded { get; set; }
        public List<Models.Person>? Persons => db?.People?.OrderBy(o=>o.Lastname).ThenBy(t=>t.Forename).ToList();
        public Models.Person SelectedPerson
        {
            get {
                return selectedPerson;
            }
            set
            {
                if (selectedPerson != null && !selectedPerson.IsNew) 
                    db.Entry(SelectedPerson).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                selectedPerson = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SaveButtonEnabled));
            }
        }
        public void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db.SaveChanges();
        }
        public bool SaveButtonEnabled => selectedPerson != null;
        #endregion
        #region logic
        private void SaveMethod() {
            try
            {
                if (string.IsNullOrEmpty(selectedPerson.Forename) || string.IsNullOrEmpty(selectedPerson.Lastname))
                    throw new ArgumentException("Forename or Lastname is empty!");
                if (selectedPerson.IsNew) db.People.Add(selectedPerson);
                db.SaveChanges();
                selectedPerson.IsNew = false;
                OnPropertyChanged(nameof(Persons));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: \r\n{ex.Message}","Saving error",MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NewMethod() {
            SelectedPerson = new Models.Person() { IsNew = true};
        }
        #endregion

        public PersonViewModel() {
            Task.Run(() =>
            {
                db = new Models.OCQSofttestContext();
                if (Persons?.Count == 0) NewMethod();
                IsLoaded = true;
                OnPropertyChanged(nameof(IsLoaded));
                OnPropertyChanged(nameof(Persons));
            });
        }
    }
}
