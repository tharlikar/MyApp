using com.minsoehanwin.sample.Core;
using com.minsoehanwin.sample.Core.EmailEntity;
using com.minsoehanwin.sample.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Repositories.EF
{
    public class EmailRepository : GenericRepository<Email>, IEmailRepository
    {
        public EmailRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public void AddOrUpdateCommit(Email email)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                AddOrUpdate(email);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error(ex.Message, ex);
                throw ex;
            }
        }
    }
}
