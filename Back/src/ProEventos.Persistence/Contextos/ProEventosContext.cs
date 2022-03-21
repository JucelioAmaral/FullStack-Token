using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Domain.Identity;

namespace ProEventos.Persistence.Contextos
{
    public class ProEventosContext : IdentityDbContext<User, Role, int, 
                                                       IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, 
                                                       IdentityRoleClaim<int>, IdentityUserToken<int>>
    // Todos esses parametros do "IdentityDbContext" são dessa classe e precisam ser
    // passados para realizar a autenticação e o uso do Token. basta clicar com CTRL + click
    {
        public ProEventosContext(DbContextOptions<ProEventosContext> options) 
            : base(options) { }
        public DbSet<Evento> tblEventos { get; set; }
        public DbSet<Lote> tblLotes { get; set; }
        public DbSet<Palestrante> tblPalestrantes { get; set; }
        public DbSet<PalestranteEvento> tblPalestrantesEventos { get; set; }
        public DbSet<RedeSocial> tblRedesSociais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Para autenticação do usuário que logar
            base.OnModelCreating(modelBuilder);// precisar passar o modelBuilder porque ele precisa ser configurado conforme abaixo.

            modelBuilder.Entity<UserRole>(userRole => 
                {
                    userRole.HasKey(ur => new { ur.UserId, ur.RoleId}); // Cria uma relacionamento entre o UserId e RoleId, ou seja, um relacionamento N:N.

                    userRole.HasOne(ur => ur.Role)
                        .WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.RoleId)
                        .IsRequired();
                    //Sentido inverso.
                    userRole.HasOne(ur => ur.User)
                        .WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.UserId)
                        .IsRequired();
                }
            );
            // Para relacionamento entre as tabelas
            modelBuilder.Entity<PalestranteEvento>()
                .HasKey(PE => new {PE.EventoId, PE.PalestranteId});

            modelBuilder.Entity<Evento>()
                .HasMany(e => e.RedesSociais)
                .WithOne(rs => rs.Evento)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Palestrante>()
                .HasMany(e => e.RedesSociais)
                .WithOne(rs => rs.Palestrante)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}