using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rocky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rocky.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<InquiryDetails> InquiryDetails { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ApplicationType> ApplicationType { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public  DbSet<InquiryCart> InquiryCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Product>().HasData(
                new Product() { Id=1,Name= "Statuary White" ,Description= "<p><span style='color:rgb(0, 62, 105);font-family:OpenSans-Regular,sans-serif; '>A sister stone to Carrara, Statuary marble features a uniform background and light gray tones with distinctive and more dramatic veining. Its semi translucent white background gives it a shiny, glossy feel, reflects light and provides a radiant finish that enhances any room.</span><br></p>",Price= 75 ,Image= "18bfc0bf16c1.png",CategoryId= 1,ApplicationTypeId= 1,ShortDesc= "A sister stone to Carrara, Statuary marble features a uniform background and light gray tones with distinctive" },
                new Product() { Id=2,Name= "Emperador Brown", Description= "<p><span style='color: rgb(0, 62, 105); font - family: OpenSans - Regular, sans - serif; '>Quarried from three regions of Spain, Emperador marble tile comes in different shades of brown, straying from the whites and grays associated with Calacatta and Carrara. It typically exhibits fine grains with irregular veins. Its darker color makes it an ideal choice for a high-traffic floors of a charming fireplace surround.</span><br></p>",Price= 84 ,Image=" b64191f0ea17.png",CategoryId = 1, ApplicationTypeId = 1,ShortDesc= "Quarried from three regions of Spain, Emperador marble tile comes in different shades of brown, straying from the whites " },
                new Product() { Id=3,Name= "Travertine beige",Description= "<p style='margin - right: 0px; margin - bottom: 30px; margin - left: 0px; font - size: 18px; font - stretch: normal; line - height: 1.56; color: rgb(0, 0, 0); font - family: &quot; Open Sans&quot;, sans - serif; '>Travertine marble is visibly porous, giving it a more natural and textured look and finish. However, when sanded down and sealed, this marble can be used for interior and exterior walls. Travertine marble is available in a variety of colours like beige, brown, white, yellow, and more.</p>",Price=90,Image= "5db7f26eec53.png",CategoryId=2,ApplicationTypeId=2,ShortDesc= "Travertine marble is visibly porous, giving it a more natural and textured look and finish. " },
                new Product() { Id=4,Name= "Calacatta",Description= "<p><span style='color: rgb(0, 0, 0); font - family: &quot; Open Sans&quot;, sans - serif; font - size: 18px; '>One of the most well-known yet rare luxury stones, Calacatta marble is often confused with Carrara since both feature grey veining. However, unlike Carrara, Calacatta showcases bolder and more dramatic veining. Calacatta white marble or Calacatta gold marble are two of the most popular picks.</span><br></p>",Price=120,Image= "197d7bd6-59f2-4916-9a35-ee2f10f44fb7.png", CategoryId = 3, ApplicationTypeId = 2 ,ShortDesc= "One of the most well-known yet rare luxury stones, Calacatta marble is often confused with Carrara since both feature grey veining." }
                );
            builder.Entity<ApplicationType>().HasData(
                new ApplicationType() { Id=1,Name= "Flooring" },
                new ApplicationType() { Id=2,Name= "Walls" },
                new ApplicationType() { Id=3,Name= "Stairs" }
                ) ;
            builder.Entity<Category>().HasData(
                new Category() { Id=1,Name= "Statuary",DisplayOrder=0 },
                new Category() { Id=2,Name= "Emperador",DisplayOrder=0 },
                new Category() { Id=3,Name= "Travertine",DisplayOrder=0 }
                );

        }
    }
}
