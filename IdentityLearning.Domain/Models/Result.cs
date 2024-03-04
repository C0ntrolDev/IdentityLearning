using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityLearning.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace IdentityLearning.Domain.Models
{
    public class Result<T> 
    {
        public bool IsSuccessfull { get; protected set; }

        private readonly T? _body;
        public T Body => IsSuccessfull ? _body! : throw new ResultNotSuccessfullException();

        private readonly IEnumerable<Error>? _errors; 
        public IEnumerable<Error> Errors => !IsSuccessfull ? _errors! : throw new ResultSuccessfullException();

        private readonly TotalErrorCode _totalErrorCode;
        public TotalErrorCode TotalErrorCode => !IsSuccessfull ? _totalErrorCode! : throw new ResultSuccessfullException();


        protected Result(T body)
        {
            IsSuccessfull = true;
            _body = body;
        }

        protected Result(IEnumerable<Error> errors, TotalErrorCode totalErrorCode = TotalErrorCode.NotSelected)
        {
            IsSuccessfull = false;
            _errors = errors;
            _totalErrorCode = totalErrorCode;
        }

        public static Result<T> Successfull(T body)
        {
            return new Result<T>(body);
        }

        public static Result<T> NotSuccessfull(IEnumerable<Error> errors,
            TotalErrorCode totalErrorCode = TotalErrorCode.NotSelected)
        {
            return new Result<T>(errors, totalErrorCode);
        }

        public static Result<T> NotSuccessfull(IEnumerable<string> errors,
            TotalErrorCode totalErrorCode = TotalErrorCode.NotSelected)
        {
            return new Result<T>(errors.Select(e => new Error("0", e)), totalErrorCode);
        }

        public static Result<T> NotSuccessfull(string error, TotalErrorCode totalErrorCode = TotalErrorCode.NotSelected) =>
            NotSuccessfull([error], totalErrorCode);


        public Result<T> WithTotalErrorCode(TotalErrorCode totalErrorCode = TotalErrorCode.NotSelected)
        {
            return IsSuccessfull 
                ? this 
                : new Result<T>(errors: this.Errors, totalErrorCode);
        }

        public static Result<T> FromIdentityResult(IdentityResult identityResult, TotalErrorCode totalErrorCode = TotalErrorCode.NotSelected)
        {
            return identityResult.Succeeded ?
                Result<T>.Successfull(body: default(T)!) :
                Result<T>.NotSuccessfull(identityResult.Errors.Select(ie => new Error(ie.Code, ie.Description)), totalErrorCode);
        }

        public override string ToString()
        {
            if (IsSuccessfull)
            {
                return $"Successfull Result of type: {nameof(T)}";
            }

            return $"Not Successfull Result of type: {nameof(T)}; " +
                   $"\n\n ErrorCode: {TotalErrorCode}; " +
                   $"\n\n Errors: {string.Join("", Errors.Select(e => $"{e.Code}:{e.Description} \n"))}";
        }
    }

    public class Result : Result<object>
    {
        protected Result(object body) : base(body)
        {
        }

        protected Result(IEnumerable<Error> errors, TotalErrorCode totalErrorCode = TotalErrorCode.NotSelected) : base(errors, totalErrorCode)
        {
        }
    }
}
