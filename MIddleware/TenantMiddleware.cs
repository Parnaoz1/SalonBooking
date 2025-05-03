using SalonBooking.Data;

namespace SalonBooking.Middleware
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, AppDbContext dbContext)
        {
            var path = context.Request.Path.Value?.Split('/');
            if (path.Length > 1)
            {
                var slug = path[1];
                var business = dbContext.Businesses.FirstOrDefault(b => b.Slug == slug);
                if (business != null)
                {
                    context.Items["Business"] = business;
                }
            }

            await _next(context);
        }
    }

}
