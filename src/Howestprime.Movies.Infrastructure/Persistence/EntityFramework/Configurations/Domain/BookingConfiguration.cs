using Howestprime.Movies.Domain.MovieEvent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configurations.Domain;

public sealed class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("Bookings");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.Id).HasConversion(
            id => id.Value,
            value => new BookingId(value));

        builder.Property(x => x.MovieEventId).IsRequired().ValueGeneratedNever();
        builder.Property(x => x.MovieEventId).HasConversion(
            id => id.Value,
            value => new MovieEventId(value));
        builder.HasOne<MovieEvent>().WithMany(e => e.Bookings).HasForeignKey(x => x.MovieEventId);

        builder.Property(x => x.BookingStatus).IsRequired();
        builder.Property(x => x.PaymentStatus).IsRequired();
        builder.Property(x => x.StandardVisitors).IsRequired();
        builder.Property(x => x.DiscountVisitors).IsRequired();

        builder.Property(x => x.SeatNumbers);
        builder.Property(x => x.SeatNumbers).HasConversion(
            list => string.Join(",", list),
            str => str.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToList());

    }
}