using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;   
        }
        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new Result(true, Messages.UserAdded);
        }

        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new Result(true, Messages.UserDeleted);
        }

        public IDataResult<User> GetById(int userid)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Id == userid));
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(),Messages.UsersListed);
        }

        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new Result(true, Messages.UserUpdated);
        }
        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        public IDataResult<User> GetByMail(string mail)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == mail));
        }
    }
}
