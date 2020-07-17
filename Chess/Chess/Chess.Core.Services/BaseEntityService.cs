using AutoMapper;
using Chess.Core.DataAccess;
using Chess.Core.DataAccess.Entities;
using Chess.Core.Domain.Interfaces;
using Chess.Core.Models;
using Chess.Core.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Chess.Core.Services
{
    public abstract class BaseCrudService<TEntity, TModel> : IBaseCrudService<TEntity, TModel>
        where TEntity : class, IBaseEntity
        where TModel : class, IBaseModel
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IDateTimeProvider _dateTimeProvider;
        protected readonly IMapper _mapper;
        protected readonly IRepository<TEntity> _repository;

        protected BaseCrudService(IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = _unitOfWork.GetRepository<TEntity>();
        }

        public async Task<TModel> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);

            return _mapper.Map<TModel>(entity);
        }

        public async Task<TModel> InsertAsync(TModel model)
        {
            var entity = _mapper.Map<TEntity>(model);
            OnBeforeInsert(entity);
            model = await SaveEntityAsync(entity);

            return model;
        }

        public async Task<TModel> UpdateAsync(TModel model)
        {
            var entity = _mapper.Map<TEntity>(model);
            OnBeforeUpdate(entity);
            model = await SaveEntityAsync(entity);

            return model;
        }

        public async Task DeleteAsync(Guid id)
            => await _repository.DeleteAsync(id);

        public async Task SaveChangesAsync()
            => await _unitOfWork.SaveChangesAsync();

        protected virtual async Task<TModel> SaveEntityAsync(TEntity entity)
            => _mapper.Map<TModel>(await _repository.SaveAsync(entity));


        protected virtual void OnBeforeUpdate(TEntity entity)
            => entity.LatestUpdateDate = _dateTimeProvider.UtcNow;

        protected virtual void OnBeforeInsert(TEntity entity)
           => entity.LatestUpdateDate = entity.CreatedDate = _dateTimeProvider.UtcNow;

        #region IDisposable Support
        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed || !disposing)
                return;

            _unitOfWork.Dispose();
            _disposed = true;
        }

        public void Dispose()
            => Dispose(true);
        #endregion
    }
}