using System.Windows;

namespace ManualGui
{
    public class SubstitutionTemplate
    {
        public char Original { get; set; }

        private string __replace { get; set; }
        public string Replace
        {
            get
            {
                return __replace;
            }
            set
            {
                if ((value.Length == 1 && char.IsLetter(value[0])) || value.Length == 0)
                {
                    __replace = value;
                }
                else
                {
                    MessageBox.Show("Letters can only be substituted for other letters!");
                }
            }
        }
    }
}
