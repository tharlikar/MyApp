using com.minsoehanwin.sample.Core.EmailEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Repositories.EF.EntityMap
{
    public class EmailEntityConfiguration : EntityTypeConfiguration<Email>
    {
        public EmailEntityConfiguration()
        {
            this.ToTable("Email");
            this.HasKey(c =>c.Id);
            this.HasMany(e => e.Bccs)
                .WithRequired(et => et.Email)
                .HasForeignKey(w => w.EmailId)
                .WillCascadeOnDelete();
            this.HasMany(e => e.Attachments)
                .WithRequired(et => et.Email)
                .HasForeignKey(w => w.EmailId)
                .WillCascadeOnDelete();
        }
    }
    public class EmailToEntityConfiguration : EntityTypeConfiguration<EmailTo>
    {
        public EmailToEntityConfiguration()
        {
            this.ToTable("EmailTo");
            this.HasKey(c => new { EmailId=c.EmailId, EmailAddress = c.EmailAddress });
            this.HasRequired(t => t.Email)
                .WithMany(e => e.EmailTos)
                .HasForeignKey(t => t.EmailId);
            
        }
    }
    public class EmailCcEntityConfiguration : EntityTypeConfiguration<EmailCc>
    {
        public EmailCcEntityConfiguration()
        {
            this.ToTable("EmailCc");
            this.HasKey(c=>new { EmailId = c.EmailId, EmailAddress = c.EmailAddress });
            this.HasRequired(t => t.Email)
                .WithMany(e => e.EmailCcs)
                .HasForeignKey(t => t.EmailId);
        }
    }
    public class EmailBccEntityConfiguration : EntityTypeConfiguration<EmailBcc>
    {
        public EmailBccEntityConfiguration()
        {
            this.ToTable("EmailBcc");
            this.HasKey(c => new { EmailId = c.EmailId, EmailAddress = c.EmailAddress });
        }
    }
    public class EmailAttachmentEntityConfiguration : EntityTypeConfiguration<EmailAttachment>
    {
        public EmailAttachmentEntityConfiguration()
        {
            this.ToTable("EmailAttachment");
            this.HasKey(c => new { EmailId = c.EmailId, FileName = c.FileName });
        }
    }
}