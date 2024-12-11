using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web.task.bueno.presentation.Repositories.Interfaces
{
    public interface IToolEncryptionRepository
    {
        string EncodePassword(string password);
        bool ValidatePassword(string password, string encodedPassword);
    }
}
