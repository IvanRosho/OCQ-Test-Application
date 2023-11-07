using System.ComponentModel.DataAnnotations.Schema;

namespace OCQ_Test_Application.Models
{
    public partial class Person : CC.Mvvm.BaseViewModel
    {
        [NotMapped]
        public string FornameForBinding {
            get => Forename;
            set { 
                Forename= value;
                OnPropertyChanged(nameof(FullName));
            }
        }
        [NotMapped]
        public string LastnameForBinding
        {
            get => Lastname;
            set
            {
                Lastname = value;
                OnPropertyChanged(nameof(FullName));
            }
        }
        [NotMapped]
        public bool IsNew;
        [NotMapped]
        public string NameRoot => $"{Lastname} {Forename}";
        [NotMapped]
        public string FullName => !IsNew && !(string.IsNullOrEmpty(Forename) || string.IsNullOrEmpty(Lastname)) ? 
            $"{Forename} {Lastname}" : "New person";
    }
}
