using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace Sluttprosjekt.Model
{
    /// <summary>
    /// Implementation of IDataContext using Linq2SQL.
    /// </summary>
    public class LinqToSqlContext : IDataContext
    {

        private readonly DataContext _context;

        public static Dictionary<Type, Type> TableMaps = new Dictionary<Type, Type>();

        public LinqToSqlContext(DataContext context)
        {
            _context = context;

            TableMaps.Add(typeof(Project), typeof(Project));
            TableMaps.Add(typeof(Member), typeof(Member));
            TableMaps.Add(typeof(Transaction), typeof(Transaction));
        }



        /// <summary>
        /// Gets the repository for the given type of entities
        /// </summary>
        /// <typeparam name="T">The type of the entity</typeparam>
        /// <returns>The repository of the given type</returns>
        public IQueryable<T> Repository<T>() where T : class
        {
            ITable table = _context.GetTable(TableMaps[typeof(T)]);
            return table.Cast<T>();
        }



        /// <summary>
        /// Deletes the specified entity from the repository
        /// </summary>
        /// <typeparam name="T">The type of the entity</typeparam>
        /// <param name="item">The entity to delete</param>
        public void Delete<T>(T item) where T : class
        {

            ITable table = _context.GetTable(TableMaps[typeof(T)]);
            table.DeleteOnSubmit(item);
        }



        /// <summary>
        /// Adds a new entity to the repository
        /// </summary>
        /// <typeparam name="T">The type of the entity</typeparam>
        /// <param name="item">The entity to add</param>
        public void Insert<T>(T item) where T : class
        {
            ITable table = _context.GetTable(TableMaps[typeof(T)]);
            table.InsertOnSubmit(item);
        }


        /// <summary>
        /// Commits the changes for this unit of work to the repository
        /// </summary>
        public void Commit()
        {
            _context.SubmitChanges();
        }



        public void Dispose()
        {
            // we don't assume to manage the lifetime of the data 
            // context, so there's nothing to dispose
        }

    }
}