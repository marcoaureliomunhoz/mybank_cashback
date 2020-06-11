using System;
using System.Collections.Generic;
using System.Text;
using MyBank.Cashback.Domain.Entities;

namespace MyBank.Cashback.Domain.Interface.Repositories
{
    public interface IClientRepository
    {
        IEnumerable<Client> ListAll();
        Client Get(int id);
        bool Insert(Client client);
    }
}
