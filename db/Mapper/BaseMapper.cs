using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiveways.Insight.Model.UnitOfWork;
using Fiveways.Logging;

namespace Fiveways.Insight.Model.Mapper
{
   public class BaseMapper
   {
        private IUnitOfWork _unitOfWork;
        private ILogger _logger;
        public IUnitOfWork UnitOfWork {
            get { return _unitOfWork; }
        }
       public ILogger Logger
       {
           get { return _logger; }
       }
        public BaseMapper(IUnitOfWork unitOfWork, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;

        }
    }
}
