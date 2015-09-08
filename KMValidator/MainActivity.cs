using Android.App;
using Android.Widget;
using Android.OS;
using Android.Text;
using Android.Text.Format;

namespace KMValidator
{
    [Activity(Label = "KMValidator", MainLauncher = true, Icon = "@drawable/car")]
    public class MainActivity : Activity
    {
        private string _validationMsg;
        private EditText _yearGot;
        private EditText _kmGot;
        private Button _button;
        private TextView _validationText;
        private string _year;
        private string _km;
        private int _calKmMin;
        private int _calKmMax; 
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            _button = FindViewById<Button>(Resource.Id.MyButton);
            _yearGot = FindViewById<EditText>(Resource.Id.Year);
            _validationText = FindViewById<TextView>(Resource.Id.validationText);
            _kmGot = FindViewById<EditText>(Resource.Id.Km);

            _yearGot.SetRawInputType(InputTypes.ClassNumber);
            _kmGot.SetRawInputType(InputTypes.ClassNumber);


            _button.Click += delegate
            {
                _validationMsg = Validation();
                _validationText.Text = "";
                _validationText.Text = string.Format("Mileage is {0}.\n Reasonable range is {1} ~ {2}.", _validationMsg, _calKmMin, _calKmMax);
            };
        }

        private string Validation()
        {
            _year = _yearGot.Text;
            _km = _kmGot.Text;
            Time now = new Time();
            now.SetToNow();
            int numValueyear;
            int numValuekm;

            int yearNow = now.Year;

            var got = int.TryParse(_year, out numValueyear);
            var value = int.TryParse(_km,out numValuekm);
   
            if (got && value)
            {
                if (numValuekm < 0 || numValueyear < 1910 || numValueyear > yearNow)
                {
                    _calKmMax = 0;
                    _calKmMin = 0;
                    return "wrong input";
                }
                var yearDiff = yearNow - numValueyear;
                _calKmMin = 10000 * yearDiff;
                _calKmMax = 20000 * yearDiff;
                if (_calKmMin < numValuekm && _calKmMax > numValuekm)
                    return "within reasonale range";
                return numValuekm <= _calKmMin ? "too low" : "too high";
            }

            return "wrong";






        }
    }
}

