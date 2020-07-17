using Chess.Core.Services.Interfaces;
using Chess.Users.DataAccess.Entities;
using Chess.Users.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chess.Users.Services.Interfaces
{
    public interface IUserService : IBaseCrudService<User, UserModel>
    {
        /// <summary>
        /// Gets a user model by a given email.
        /// </summary>
        /// <param name="email">An email of an exsiting user</param>
        /// <returns>Returns the user with the given email.</returns>
        Task<UserModel> GetByEmailAsync(string email);

        /// <summary>
        /// Checks if a user if this email already exists.
        /// </summary>
        /// <param name="email">The email of the new user.</param>
        /// <returns>True of False depending on whether a user with this email already exists.</returns>
        Task<bool> DoesUserExistAsync(string email);

        /// <summary>
        /// Changes the current password of a user with a new one
        /// </summary>
        Task ChangePasswordAsync(Guid userId, ChangePasswordModel model);

        /// <summary>
        /// Return the user details for a certain user
        /// </summary>
        Task<UserDetailsModel> GetUserDetailsAsync(Guid userId);

        /// <summary>
        /// Updates user details
        /// </summary>
        Task<UserDetailsModel> UpdateDetailsAsync(Guid userId, UserDetailsModel model);

        /// <summary>
        /// Update the points of a user
        /// </summary>
        Task UpdatePointsAsync(Guid userId, PointsUpdateModel model);

        /// <summary>
        /// Returns the user details for all non deleted users
        /// </summary>
        Task<IEnumerable<UserDetailsModel>> GetAllUserDetailsAsync();
    }
}