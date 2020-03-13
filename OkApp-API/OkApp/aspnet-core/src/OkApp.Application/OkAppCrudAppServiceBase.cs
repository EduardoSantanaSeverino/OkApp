using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using OkApp.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OkApp.Authorization.Users;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Identity;
using Abp.IdentityFramework;
using Abp.Extensions;

namespace OkApp
{
    public class OkAppCrudAppServiceBase<TEntity, TEntityDto, TPrimaryKey, TRequestDto, TCreateDto, TUpdateDto>
        : AsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TRequestDto, TCreateDto, TUpdateDto>,

        IOkAppCrudAppServiceBase<TEntityDto, TPrimaryKey, TRequestDto, TCreateDto, TUpdateDto>
         where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TCreateDto : IEntityDto<TPrimaryKey>
        where TUpdateDto : IEntityDto<TPrimaryKey>

    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        protected OkAppCrudAppServiceBase(IRepository<TEntity, TPrimaryKey> repository) : base(repository)
        {
            LocalizationSourceName = OkAppConsts.LocalizationSourceName;
        }

        protected RetType GetPropertyValue<RetType>(object input, string propertyName)
        {
            foreach (var prop in input.GetType().GetProperties())
            {
                if (prop.Name == propertyName)
                {
                    var _value = prop.GetValue(input);
                    if (_value == null)
                    {
                        return default(RetType);
                    }
                    return ChangeType<RetType>(_value); 
                }
            }

            return default(RetType);
        }

        protected override IQueryable<TEntity> CreateFilteredQuery(TRequestDto input)
        {
            var queryable = base.CreateFilteredQuery(input);
            var keyword = GetPropertyValue<string>(input, "Keyword");
            var isActive = GetPropertyValue<bool?>(input, "IsActive");

            if (isActive.HasValue)
            {
                var filterActive = GetFilterFromPropertyName(isActive, "IsActive", typeof(bool));
                queryable = queryable.Where(filterActive);
            }
            if (!keyword.IsNullOrWhiteSpace())
            {
                foreach (string item in PropertyNamesForSearchOnFilter())
                {
                    var filterActive = GetFilterFromPropertyName(keyword, item, typeof(string));
                    queryable = queryable.Where(filterActive);
                }
            }

            return queryable;
        }

        private static T ChangeType<T>(object value)
        {
            var t = typeof(T);

            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return default(T);
                }

                t = Nullable.GetUnderlyingType(t);
            }

            return (T)Convert.ChangeType(value, t);
        }


        protected Expression<Func<TEntity, bool>> GetFilterFromPropertyName(object obj, string propertyName, Type type)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "t");
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            ConstantExpression _value = Expression.Constant(obj, type);
            Expression<Func<TEntity, bool>> filter = null;
            if (type.Name.ToLower().Contains("string"))
            {
                MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var containsMethodExp = Expression.Call(member, method, _value);
                filter = Expression.Lambda<Func<TEntity, bool>>(containsMethodExp, parameter);
            }
            else
            {
                filter = Expression.Lambda<Func<TEntity, bool>>(Expression.Equal(member, _value), new ParameterExpression[] { parameter });
            }
           
            return filter;
        }

        protected virtual List<string> PropertyNamesForSearchOnFilter()
        {
            return new List<string> { "Description" };
        }

        [AbpAllowAnonymous]
        public async Task<List<TEntityDto>> GetAllForCombo()
        {
            var parameter = Expression.Parameter(typeof(TEntity), "t");
            var member = Expression.PropertyOrField(parameter, "IsActive");
            var _value = Expression.Constant(true, typeof(bool));
            var filter = Expression.Lambda<Func<TEntity, bool>>(Expression.Equal(member, _value), new ParameterExpression[] { parameter });

            var listResult = Repository.GetAll().Where(filter);
            listResult = FilterWhere(listResult);

            var result = await Task.Run<List<TEntityDto>>(() =>
                ObjectMapper.Map<List<TEntityDto>>(listResult.ToList())
            );

            return result;
        }

        [AbpAllowAnonymous]
        protected virtual IQueryable<TEntity> FilterWhere(IQueryable<TEntity> queryable)
        {
            var filters = FilterWhere<TEntity>();
            foreach (var filter in filters)
                queryable = queryable.Where(filter);

            return queryable;
        }

        [AbpAllowAnonymous]
        protected virtual List<Expression<Func<T, bool>>> FilterWhere<T>()
        {
            
            List<Expression<Func<T, bool>>> filters = new List<Expression<Func<T, bool>>>();
            //TODO: FIND A WAY TO GET SAME INFORMATION ON CORE
            var collection = new NameValueCollection(); //System.Web.HttpContext.Current.Request.QueryString; // mayby => Microsoft.AspNetCore.Http.HttpContext.Current.Request.QueryString;
            foreach (var item in collection.AllKeys)
            {
                try
                {
                    object value = null;
                    var property = ObjectType(typeof(T), item, collection[item], out value);

                    if (property == null) continue;

                    var parameter = Expression.Parameter(typeof(T), "t");
                    var member = Expression.PropertyOrField(parameter, property.Name);
                    var _value = Expression.Constant(value, property.PropertyType);
                    var filter = Expression.Lambda<Func<T, bool>>(Expression.Equal(member, _value), new ParameterExpression[] { parameter });
                    filters.Add(filter);

                }
                catch (Exception ex)
                {
                    throw;
                }

            }
            return filters;
        }

        [AbpAllowAnonymous]
        PropertyInfo ObjectType(Type _type, string name, object _value, out object value)
        {
            value = _value;
            var property = _type.GetProperties().FirstOrDefault(t => string.Compare(t.Name, name, true) == 0);

            if (property == null) return null;

            Type propertyType = property.PropertyType;

            if (propertyType.IsGenericType && propertyType.Name == typeof(Nullable<>).Name)
                propertyType = propertyType.GetGenericArguments()[0];

            if (propertyType == typeof(DateTime))
            {
                string[] formats = { "yyyy-MM-ddTHH:mm:ss.000Z", "yyyy-MM-dd HH:mm:ss", "yyyy-MM-dd", "yyyy-MM-ddTHH:mm:ss" };
                value = DateTime.ParseExact(_value.ToString(), formats, CultureInfo.InvariantCulture, DateTimeStyles.None);
            }
            else if (propertyType == typeof(int))
                value = Convert.ToInt32(_value);
            else if (propertyType == typeof(long))
                value = Convert.ToInt64(_value);
            else if (propertyType == typeof(decimal))
                value = Convert.ToDecimal(_value);
            else if (propertyType == typeof(float))
                value = Convert.ToDouble(_value);
            else if (propertyType == typeof(bool))
                value = Convert.ToBoolean(_value);

            return property;
        }

        protected virtual Task<User> GetCurrentUserAsync()
        {
            var user = UserManager.FindByIdAsync(AbpSession.UserId.ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

    }

}

