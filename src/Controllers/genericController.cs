using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_app.Core;

namespace web_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class genericController : ControllerBase
    {
        private IFirebirdDbContext _context;

        public genericController(IFirebirdDbContext context)
        {
            _context = context;
        }

        // GET: api/generic/columns/{table}
        [HttpGet("columns/{table}")]
        public dynamic GetColumnList(string table)
        {
            return _context.Query($"SELECT RDB$FIELD_NAME FROM RDB$RELATION_FIELDS WHERE RDB$RELATION_NAME = '{table}'; ");
        }

        // GET: api/generic/get_all_records/{table}
        [HttpGet("get_all_records/{table}")]
        public dynamic SelectAllFrom(string table)
        {
            return _context.ArrangedQuery($"SELECT * FROM {table}");
        }

        // GET: api/generic/get_all_records_filtered/{table}/{column}/{id}
        [HttpGet("get_all_records_filtered/{table}/{column}/{id}")]
        public dynamic SelectAllFromWhere(string table, string column, int id)
        {
            return _context.ArrangedQuery($"SELECT * FROM {table} WHERE {column}={id}");
        }
    }
}
