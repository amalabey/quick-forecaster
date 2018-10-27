using QuickForecaster.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickForecaster.Application.UnitTests.Infrastructure
{
    public class TestClientDataBuilder
    {
        private int _index;
        private string _clientName;
        private string _accountManagerEmail;
        private string _accountManagerName;


        public TestClientDataBuilder WithDummyClient(int index)
        {
            _index = index;
            _clientName = $"Contoso_{index}";
            return this;
        }

        public TestClientDataBuilder WithDummyAccountManager(int index)
        {
            _accountManagerEmail = $"jdoe{index}@contoso.com";
            _accountManagerName = $"John{index} Doe";
            return this;
        }

        public Client Build()
        {
            return new Client
            {
                //Id = _index + 1,
                Name = _clientName,
                AccountManager = new Domain.ValueObjects.Contact(_accountManagerEmail, _accountManagerName)
            };
        }
    }
}
