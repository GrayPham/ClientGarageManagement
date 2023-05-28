namespace ManagementStore.Model
{
    public class FormattedPhoneCode
    {
        public string Code { get; }
        public string Display { get; }

        public FormattedPhoneCode(string code, string display)
        {
            Code = code;
            Display = display;
        }
    }
}