using QuickForecaster.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickForecaster.Domain.ValueObjects
{
    public class Contact:ValueObject
    {
        private Contact()
        {
        }

        public Contact(string email, string displayName)
        {
            this.DisplayName = displayName;
            this.Email = email;
        }

        public string Email { get; private set; }

        public string DisplayName { get; private set; }

        public override string ToString()
        {
            return this.DisplayName;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Email;
            yield return DisplayName;
        }
    }
}
