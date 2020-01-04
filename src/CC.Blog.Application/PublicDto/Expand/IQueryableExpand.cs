using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Linq.Expressions;

namespace CC.Blog.PublicDto.Expand
{
    public static class IQueryableExpand
    {
        /// <summary>
        /// 如果<param name="condition">为真，则执行<param name="predicate"></param>过滤
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selectDto"></param>
        /// <returns></returns>
        public static IQueryable<T> WherePaging<T>(this IQueryable<T> source, IPagingSelectDto selectDto)
        {
            return source.Page(selectDto.Page, selectDto.PageSize);
        }

        /// <summary>
        /// 根据创建时间进过滤
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="selectDto"></param>
        /// <returns></returns>
        public static IQueryable<T> WhereTimeFrame<T>(this IQueryable<T> source, ITimeFrameSelectDto selectDto) where T : Abp.Domain.Entities.Auditing.ICreationAudited
        {
            return source.WhereIf(selectDto.StartTime.HasValue, p => p.CreationTime >= selectDto.StartTime.Value)
                .WhereIf(selectDto.EndTime.HasValue, p => p.CreationTime <= selectDto.EndTime.Value);
        }
    }
}
