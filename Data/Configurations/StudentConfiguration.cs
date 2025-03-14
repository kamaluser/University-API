﻿using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(x => x.FullName).IsRequired(true).HasMaxLength(35);
            builder.Property(x => x.Email).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.BirthDate).IsRequired(true).HasColumnType("date");
            builder.HasOne(x => x.Group).WithMany(x => x.Students).HasForeignKey(x => x.GroupId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}