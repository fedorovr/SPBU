namespace RegexValidator
{
    class Validator
    {
        private const string Nickname = @"^[_A-z][-_\w]{0,50}([.][-_\w]{1,50}){0,10}";
        private const string Domain = @"[\w][-\w]{0,50}([.][\w][-\w]{1,50}){0,10}";
        private const string DomainZone = @"[A-z]{2,15}$";
        private const string RuZip = @"^([\d]){6}$";
        private const string UsZip = @"^([\d]){5}([-]([\d]){4})?$";
        private const string RuPhoneCode = @"^(\+7|8)?((\()?[\d]{3}(\))?)";
        private const string RuPhoneMainPart = @"[\d]{3}(-)?([\d]{2}(-)?){2}$";
        private const string EmergencyPhone = @"^01|02|03|04|112$";

        private const string ResultEmail = "It's a valid e-mail adress";
        private const string ResultZip = "It's a valid zip code";
        private const string ResultPhone = "It's a valid phone number";
        private const string ResultInvalid = "It's an invalid input";

        public static string Validate(string input)
        {
            var emailRegex = new System.Text.RegularExpressions.Regex(Nickname + @"[@]" + Domain + @"[.]" + DomainZone);
            var ruZipRegex = new System.Text.RegularExpressions.Regex(RuZip);
            var usZipRegex = new System.Text.RegularExpressions.Regex(UsZip);
            var phoneRegex = new System.Text.RegularExpressions.Regex(RuPhoneCode + RuPhoneMainPart);
            var emergencyPhoneRegex = new System.Text.RegularExpressions.Regex(EmergencyPhone);

            if (emailRegex.IsMatch(input))
            {
                return ResultEmail;
            }
            else if (ruZipRegex.IsMatch(input) || usZipRegex.IsMatch(input))
            {
                return ResultZip;
            }
            else if (phoneRegex.IsMatch(input) || emergencyPhoneRegex.IsMatch(input))
            {
                return ResultPhone;
            }
            else
            {
                return ResultInvalid;
            }
        }
    }
}
