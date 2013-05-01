using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sluttprosjekt.Model
{
    [Database]
    class SpleiselagDataContext : DataContext
    {
        public Table<Project> Projects; 
        public Table<Member> Members;
        public Table<Transaction> Transactions;

        public SpleiselagDataContext(string connection)
            : base(connection) { }
    }

    public interface IDataContext : IDisposable
    {

        /// <summary>
        /// Gets the repository for the given type of entities
        /// </summary>
        /// <typeparam name="T">The type of the entity</typeparam>
        /// <returns>The repository of the given type</returns>
        IQueryable<T> Repository<T>() where T : class;


        /// <summary>
        /// Adds a new entity to the repository
        /// </summary>
        /// <typeparam name="T">The type of the entity</typeparam>
        /// <param name="item">The entity to add</param>
        void Insert<T>(T item) where T : class;


        /// <summary>
        /// Deletes the specified entity from the repository
        /// </summary>
        /// <typeparam name="T">The type of the entity</typeparam>
        /// <param name="item">The entity to delete</param>
        void Delete<T>(T item) where T : class;



        /// <summary>
        /// Commits the changes to the repository
        /// </summary>
        void Commit();

    }

    public class LinqToSqlContext : IDataContext
    {

        private readonly DataContext _context;

        public static Dictionary<Type, Type> TableMaps = new Dictionary<Type, Type>();

        public LinqToSqlContext(DataContext context)
        {
            _context = context;
            TableMaps.Add(typeof(ITransaction), typeof(Transaction));
            TableMaps.Add(typeof(IMember), typeof(Member));
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
