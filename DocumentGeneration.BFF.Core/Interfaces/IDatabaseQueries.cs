using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentGeneration.BFF.Core.Interfaces
{
    public interface IDatabaseQueries
    {
        public Task checkUser(string userName);

    }
}
