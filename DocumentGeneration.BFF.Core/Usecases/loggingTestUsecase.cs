using DocumentGeneration.BFF.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentGeneration.BFF.Core.Usecases
{
    internal class loggingTestUsecase : ITestCase
    {
        public loggingTestUsecase(ILogger<loggingTestUsecase> logger) {
            _logger = logger;
        }

        private readonly ILogger<loggingTestUsecase> _logger;
        public void logSomething(string message)
        {
            _logger.LogError(message);
        }


    }
}
