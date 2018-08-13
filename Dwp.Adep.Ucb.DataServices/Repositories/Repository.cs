using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Linq.Expressions;
using Dwp.Adep.Ucb.IoC.ServiceLocation;
using Dwp.Adep.Ucb.DataServices.Repositories;
using System.Data.Objects.DataClasses;
using System.Data;
using System.Configuration;
using System.Data.Entity;
using System.Reflection;
using Dwp.Adep.Ucb.DataServices.Models;

namespace Dwp.Adep.Ucb.DataServices
{
    public class Repository<T> : IRepository<T> where T : class, IAuditable
    {
        protected readonly IObjectContext _objectContext;
        protected readonly IObjectSet<T> _objectSet;
        protected readonly IObjectSet<Audit> _objectSetAudit;
        protected readonly string _userName;

        public Repository(string userName, string userProxied, string appID, string level)
            : this(new NullObjectContext(), userName, userProxied, appID, level)
        {
        }

        public Repository(IObjectContext objectContext, string userName, string userProxied, string appID, string level)
        {
            if (!(objectContext is NullObjectContext))
            {
                _objectContext = objectContext;
            }
            else
            {
                _objectContext = SimpleServiceLocator.Instance.Get<IObjectContext>();
            }

            _objectSet = _objectContext.CreateObjectSet<T>();
            _objectSetAudit = _objectContext.CreateObjectSet<Audit>();
            _userName = userName;

        }

        #region All the Gets

        public IQueryable<T> GetQuery()
        {
            return _objectSet;
        }

        public IEnumerable<T> GetAll()
        {
            return GetQuery().AsEnumerable();
        }

        public IEnumerable<T> GetAll(params string[] children)
        {
            return EagerQuery(children).ToList();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, object>> sortExpression, params string[] children)
        {
            #region Parameter validation
            // Validate parameters
            if (null == sortExpression) throw new ArgumentOutOfRangeException("sortExpression");
            #endregion
            var query = EagerQuery(children).OrderBy(sortExpression);
            return query.ToList();
        }

        public IEnumerable<T> Find(ISpecification<T> specification)
        {

            #region Parameter validation
            // Validate parameters
            if (null == specification) throw new ArgumentOutOfRangeException("specification");
            #endregion

            return specification.SatisfyingEntitiesFrom(GetQuery());
        }

        public IEnumerable<T> Find(ISpecification<T> specification, Expression<Func<T, object>> sortExpression)
        {
            #region Parameter validation
            // Validate parameters
            if (null == specification) throw new ArgumentOutOfRangeException("specification");
            if (null == sortExpression) throw new ArgumentOutOfRangeException("sortExpression");
            #endregion
            return Find(specification, sortExpression, true);
        }

        //OrderBy versions of Find
        public IEnumerable<T> Find(ISpecification<T> specification, Expression<Func<T, object>> sortExpression, bool isAscending)
        {
            #region Parameter validation
            // Validate parameters
            if (null == specification) throw new ArgumentOutOfRangeException("specification");
            if (null == sortExpression) throw new ArgumentOutOfRangeException("sortExpression");
            #endregion
            var query = specification.SatisfyingEntitiesFrom(GetQuery());
            query = (isAscending ? query.OrderBy(sortExpression) : query.OrderByDescending(sortExpression));
            return query;
        }

        public IEnumerable<T> Find(ISpecification<T> specification, Expression<Func<T, object>> sortExpression, params string[] children)
        {
            #region Parameter validation
            // Validate parameters
            if (null == specification) throw new ArgumentOutOfRangeException("specification");
            if (null == sortExpression) throw new ArgumentOutOfRangeException("sortExpression");
            #endregion
            return Find(specification, sortExpression, true, children);
        }

        public IEnumerable<T> Find(ISpecification<T> specification, Expression<Func<T, object>> sortExpression, bool isAscending, params string[] children)
        {

            #region Parameter validation
            // Validate parameters
            if (null == specification) throw new ArgumentOutOfRangeException("specification");
            if (null == sortExpression) throw new ArgumentOutOfRangeException("sortExpression");
            #endregion
            var query = specification.SatisfyingEntitiesFrom(EagerQuery(children));
            query = (isAscending ? query.OrderBy(sortExpression) : query.OrderByDescending(sortExpression));
            return query;
        }

        //Paging versions of Find
        public IEnumerable<T> Find(ISpecification<T> specification, Expression<Func<T, object>> sortExpression, int page, int pageSize)
        {
            #region Parameter validation
            // Validate parameters
            if (null == specification) throw new ArgumentOutOfRangeException("specification");
            if (null == sortExpression) throw new ArgumentOutOfRangeException("sortExpression");
            #endregion
            return Find(specification, sortExpression, true, page, pageSize);
        }
        public IEnumerable<T> Find(ISpecification<T> specification, Expression<Func<T, object>> sortExpression, bool isAscending, int page, int pageSize)
        {
            #region Parameter validation
            // Validate parameters
            if (null == specification) throw new ArgumentOutOfRangeException("specification");
            if (null == sortExpression) throw new ArgumentOutOfRangeException("sortExpression");
            #endregion
            var query = specification.SatisfyingEntitiesFrom(GetQuery());
            query = (isAscending ? query.OrderBy(sortExpression) : query.OrderByDescending(sortExpression));
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<T> Find(ISpecification<T> specification, Expression<Func<T, object>> sortExpression, int page, int pageSize, params string[] children)
        {

            #region Parameter validation
            // Validate parameters
            if (null == specification) throw new ArgumentOutOfRangeException("specification");
            if (null == sortExpression) throw new ArgumentOutOfRangeException("sortExpression");
            #endregion
            return Find(specification, sortExpression, true, page, pageSize, children);
        }

        public IEnumerable<T> Find(ISpecification<T> specification, Expression<Func<T, object>> sortExpression, bool isAscending, int page, int pageSize, params string[] children)
        {
            #region Parameter validation
            // Validate parameters
            if (null == specification) throw new ArgumentOutOfRangeException("specification");
            if (null == sortExpression) throw new ArgumentOutOfRangeException("sortExpression");
            #endregion
            var query = specification.SatisfyingEntitiesFrom(EagerQuery(children));
            query = (isAscending ? query.OrderBy(sortExpression) : query.OrderByDescending(sortExpression));
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> filter)
        {
            #region Parameter validation
            // Validate parameters
            if (null == filter) throw new ArgumentOutOfRangeException("filter");
            #endregion
            return _objectSet.Where(filter);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> filter, params string[] children)
        {

            #region Parameter validation
            // Validate parameters
            if (null == filter) throw new ArgumentOutOfRangeException("filter");
            #endregion
            return EagerQuery(children).Where(filter);

        }

        public T Single(ISpecification<T> specification)
        {

            #region Parameter validation
            // Validate parameters
            if (null == specification) throw new ArgumentOutOfRangeException("specification");
            #endregion
            return specification.SatisfyingEntityFrom(GetQuery());
        }

        public T Single(ISpecification<T> specification, params string[] children)
        {
            #region Parameter validation
            // Validate parameters
            if (null == specification) throw new ArgumentOutOfRangeException("specification");
            #endregion
            return specification.SatisfyingEntityFrom(EagerQuery(children));
        }

        public T Single(Expression<Func<T, bool>> filter)
        {
            #region Parameter validation
            // Validate parameters
            if (null == filter) throw new ArgumentOutOfRangeException("filter");
            #endregion
            return _objectSet.Single(filter);
        }

        public T Single(Expression<Func<T, bool>> filter, params string[] children)
        {
            #region Parameter validation
            // Validate parameters
            if (null == filter) throw new ArgumentOutOfRangeException("filter");
            #endregion
            return EagerQuery(children).Single(filter);
        }

        public T Find(Guid code)
        {
            return _objectSet.SingleOrDefault((e) => e.Code.Equals(code));
        }

        protected IQueryable<T> EagerQuery(params string[] children)
        {
            IQueryable<T> query = (IQueryable<T>)_objectSet;

            foreach (string child in children)
            {
                query = query.Include(child);

            }
            return query;
        }

        #endregion

        #region Delete

        public void Delete(T entity)
        {           
            //attach as modified as may already be in the context
            _objectSet.AttachAsModified(entity, true);
            _objectSet.DeleteObject(entity);
        }
        #endregion

        #region Update

        public void Update(T entity)
        {           
            _objectSet.AttachAsModified(entity, true);
        }

        public void UpdateWith(T entity, T old)
        {           
            _objectSet.AttachAsModified(entity, old, true);
        }
        #endregion
        #region Add
        public void Add(T entity)
        {          
            _objectSet.AddObject(entity);
        }
        #endregion
        public int Count(ISpecification<T> specification)
        {
            return specification.SatisfyingEntitiesFrom(GetQuery()).Count();
        }

        #region Audit
        private void AuditLog(Audit audit)
        {
            _objectSetAudit.AddObject(audit);
        }
        #endregion
    }
}
