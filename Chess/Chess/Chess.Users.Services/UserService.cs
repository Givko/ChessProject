using AutoMapper;
using Chess.Core.DataAccess;
using Chess.Core.Domain.Interfaces;
using Chess.Core.Services;
using Chess.Users.DataAccess.Entities;
using Chess.Users.Models.UserModels;
using Chess.Users.Services.Exceptions;
using Chess.Users.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chess.Users.Services
{
    public class UserService : BaseCrudService<User, UserModel>, IUserService
    {
        public UserService(IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            IMapper mapper)
            : base(unitOfWork, dateTimeProvider, mapper) { }

        public async Task<UserModel> GetByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException("email");

            var user = await _repository.Get(u => u.Email == email);

            if (user == default)
                return default;

            return _mapper.Map<UserModel>(user);
        }

        public async Task<bool> DoesUserExistAsync(string email)
            => await _repository.AnyAsync(u => u.Email == email);

        protected override void OnBeforeInsert(User entity)
        {
            base.OnBeforeInsert(entity);

            entity.Password = PasswordHasher.HashPassword(entity.Password);
        }

        public async Task ChangePasswordAsync(Guid userId, ChangePasswordModel model)
        {
            var user = await _repository.GetByIdAsync(userId);

            if (!PasswordHasher.VerifyPassword(model.OldPassword, user.Password))
                throw new ChangePasswordException("Old password is not correct.");

            user.Password = PasswordHasher.HashPassword(model.NewPassword);

            OnBeforeUpdate(user);
            await SaveEntityAsync(user);
        }

        public async Task<UserDetailsModel> GetUserDetailsAsync(Guid userId)
        {
            if (userId == default)
                throw new ArgumentNullException(nameof(userId));

            var userDetailsDto = await _repository.GetDto(u => u.Id == userId, u => new UserDetailsModel
            {
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Points = u.Points,
                Username = u.Username
            });

            return userDetailsDto;
        }

        public async Task<UserDetailsModel> UpdateDetailsAsync(Guid userId, UserDetailsModel model)
        {
            var user = await _repository.GetByIdAsync(userId);

            _mapper.Map(model, user);
            user = await _repository.SaveAsync(user);
            _mapper.Map(user, model);

            return model;
        }

        public async Task UpdatePointsAsync(Guid userId, PointsUpdateModel model)
        {
            if (userId == default)
                throw new ArgumentNullException(nameof(userId));

            var user = await _repository.GetByIdAsync(userId);
            user.Points = model.Points;
            await _repository.SaveAsync(user);
        }

        public async Task<IEnumerable<UserDetailsModel>> GetAllUserDetailsAsync()
            => await _repository.GetAllDtos(u => new UserDetailsModel
            {
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Points = u.Points,
                Username = u.Username
            });
    }
}