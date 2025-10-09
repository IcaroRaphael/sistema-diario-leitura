using Microsoft.EntityFrameworkCore;
using SistemaDiarioLeitura.Models;

namespace SistemaDiarioLeitura.Context;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autor> Autores { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Leitura> Leituras { get; set; }

    public virtual DbSet<Livro> Livros { get; set; }

    public virtual DbSet<StatusLeitura> StatusLeituras { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Autor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("autor");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Biografia)
                .HasColumnType("text")
                .HasColumnName("biografia");
            entity.Property(e => e.DataNascimento).HasColumnName("data_nascimento");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("genero");

            entity.HasIndex(e => e.Nome, "uk_genero_nome").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<Leitura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("leitura");

            entity.HasIndex(e => e.IdLivro, "fk_leitura_livro");

            entity.HasIndex(e => e.IdStatusLeitura, "fk_leitura_status_leitura");

            entity.HasIndex(e => e.IdUsuario, "fk_leitura_usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comentario)
                .HasMaxLength(500)
                .HasColumnName("comentario");
            entity.Property(e => e.DataFim).HasColumnName("data_fim");
            entity.Property(e => e.DataInicio).HasColumnName("data_inicio");
            entity.Property(e => e.IdLivro).HasColumnName("id_livro");
            entity.Property(e => e.IdStatusLeitura).HasColumnName("id_status_leitura");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Nota).HasColumnName("nota");

            entity.HasOne(d => d.IdLivroNavigation).WithMany(p => p.Leituras)
                .HasForeignKey(d => d.IdLivro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_leitura_livro");

            entity.HasOne(d => d.IdStatusLeituraNavigation).WithMany(p => p.Leituras)
                .HasForeignKey(d => d.IdStatusLeitura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_leitura_status_leitura");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Leituras)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("fk_leitura_usuario");
        });

        modelBuilder.Entity<Livro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("livro");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AnoPublicacao).HasColumnName("ano_publicacao");
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .HasColumnName("titulo");

            entity.HasMany(d => d.IdAutors).WithMany(p => p.IdLivros)
                .UsingEntity<Dictionary<string, object>>(
                    "LivroAutor",
                    r => r.HasOne<Autor>().WithMany()
                        .HasForeignKey("IdAutor")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_livro_autor_autor"),
                    l => l.HasOne<Livro>().WithMany()
                        .HasForeignKey("IdLivro")
                        .HasConstraintName("fk_livro_autor_livro"),
                    j =>
                    {
                        j.HasKey("IdLivro", "IdAutor")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("livro_autor");
                        j.HasIndex(new[] { "IdAutor" }, "fk_livro_autor_autor");
                        j.IndexerProperty<int>("IdLivro").HasColumnName("id_livro");
                        j.IndexerProperty<int>("IdAutor").HasColumnName("id_autor");
                    });

            entity.HasMany(d => d.IdGeneros).WithMany(p => p.IdLivros)
                .UsingEntity<Dictionary<string, object>>(
                    "LivroGenero",
                    r => r.HasOne<Genero>().WithMany()
                        .HasForeignKey("IdGenero")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_livro_genero_genero"),
                    l => l.HasOne<Livro>().WithMany()
                        .HasForeignKey("IdLivro")
                        .HasConstraintName("fk_livro_genero_livro"),
                    j =>
                    {
                        j.HasKey("IdLivro", "IdGenero")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("livro_genero");
                        j.HasIndex(new[] { "IdGenero" }, "fk_livro_genero_genero");
                        j.IndexerProperty<int>("IdLivro").HasColumnName("id_livro");
                        j.IndexerProperty<sbyte>("IdGenero").HasColumnName("id_genero");
                    });
        });

        modelBuilder.Entity<StatusLeitura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("status_leitura");

            entity.HasIndex(e => e.Nome, "uk_status_leitura_nome").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nome)
                .HasMaxLength(20)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.Email, "uk_usuario_email").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataCadastro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("data_cadastro");
            entity.Property(e => e.Email)
                .HasMaxLength(320)
                .HasColumnName("email");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .HasColumnName("nome");
            entity.Property(e => e.Senha)
                .HasMaxLength(255)
                .HasColumnName("senha");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
