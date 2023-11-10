﻿using Domain.Common.Interfaces;

namespace Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity, IAuditableEntity
{
    public Guid? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}