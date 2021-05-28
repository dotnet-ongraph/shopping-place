using Core.Base;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Infrastructure.Filters
{
    public class ValidateEntityExistAttribute<TEntity> : IActionFilter where TEntity : BaseEntity
    {
        private readonly IEntityRepository<TEntity> entityRepository;
        
        public ValidateEntityExistAttribute(IEntityRepository<TEntity> entity)
        {
            entityRepository = entity;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            int id;
            if (context.ActionArguments.ContainsKey("id"))
            {
                id = (int)context.ActionArguments["id"];
            }
            else
            {
                context.Result = new BadRequestObjectResult("id parameter does not exist.");
                return;
            }
            var entity = entityRepository.Get(id);
            if (entity == null)
            {
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("entity", entity);
            }
        }
    }
}