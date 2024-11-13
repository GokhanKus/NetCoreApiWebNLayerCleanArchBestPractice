﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace App.Repositories.Interceptors;
public class AuditDbContextInterceptor : SaveChangesInterceptor
{
	public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
	{
		foreach (var entityEntry in eventData.Context!.ChangeTracker.Entries().ToList())
		{
			if (entityEntry.Entity is not IAuditEntity auditEntity) continue;

			if (entityEntry.State is not (EntityState.Added or EntityState.Modified)) continue;

			//delegate ile daha iyi yöntem
			Behaviours[entityEntry.State](eventData.Context, auditEntity);
			#region Switch&Case ile yontem
			//switch (entityEntry.State)
			//{
			//	case EntityState.Added:

			//		AddBehaviour(eventData.Context, auditEntity);
			//		break;

			//	case EntityState.Modified:

			//		ModifiedBehaviour(eventData.Context, auditEntity);
			//		break;
			//}
			#endregion

		}
		return base.SavingChangesAsync(eventData, result, cancellationToken);
	}

	private static void ModifiedBehaviour(DbContext context, IAuditEntity auditEntity)
	{
		auditEntity.Updated = DateTime.Now;
		context.Entry(auditEntity).Property(x => x.Created).IsModified = false;
	}

	private static void AddBehaviour(DbContext context, IAuditEntity auditEntity)
	{
		auditEntity.Created = DateTime.Now;
		context.Entry(auditEntity).Property(x => x.Updated).IsModified = false;
	}

	private static readonly Dictionary<EntityState, Action<DbContext, IAuditEntity>> Behaviours = new()
	{
		{EntityState.Added,AddBehaviour},
		{EntityState.Modified,ModifiedBehaviour},
	};
}
