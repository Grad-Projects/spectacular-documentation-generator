using DocumentGeneration.BFF.Core.Interfaces;
using DocumentGeneration.BFF.Core.Operations;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentGeneration.BFF.Core.Usecases
{
    public class CheckUserInDBUsecase : IDatabaseQueries
    {
        private readonly checkUserInDB _checkUser;

        public CheckUserInDBUsecase(checkUserInDB checkUser)
        {
            _checkUser = checkUser;
        }

        public async Task checkUser(string userName)
        {
            await _checkUser(userName);
        }
    }
}
