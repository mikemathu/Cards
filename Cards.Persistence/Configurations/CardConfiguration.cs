using Cards.Domain.Constants;
using Cards.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.Persistence.Configurations
{
    internal sealed class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(card => card.CardId);

            builder.Property(card => card.CardId).HasColumnName("CardId");

            builder.Property(card => card.CardId).HasMaxLength(50);

            builder.Property(card => card.CardId).ValueGeneratedOnAdd();

            builder.Property(card => card.Name).IsRequired().HasMaxLength(50);

            builder.Property(card => card.Description).IsRequired(false).HasMaxLength(255);

            builder.Property(card => card.DateOfCreation).IsRequired();

            builder.Property(card => card.Color).IsRequired(false).HasMaxLength(7);


            builder.HasData
                (
                    new Card
                    {
                        CardId = "fixbugs2-cd1d-43cd-b997-71a7f2a20096",
                        Name = "Fix bugs",
                        Description = "The system has bags to be fixed",
                        DateOfCreation = new DateTime(2024, 1, 20, 20, 37, 19, 0, DateTimeKind.Utc),
                        StatusId = StatusDetails.StatusNameToIdMappings[StatusDetails.ToDo],
                        AppUserId = "kev5f943-112f-4d49-888d-c671e210b8b8",
                        Color = "#ADD8E6"//Light Blue
                    },
                    new Card
                    {
                        CardId = "systemInstallation-8fae-7488fc2c1b95",
                        Name = "System Installation",
                        Description = "Installation of system to the new client.",
                        DateOfCreation = new DateTime(2024, 1, 15, 20, 37, 19, 0, DateTimeKind.Utc),
                        StatusId = StatusDetails.StatusNameToIdMappings[StatusDetails.InProgress],
                        AppUserId = "kev5f943-112f-4d49-888d-c671e210b8b8",
                        Color = "#FF7F50" //Coral
                    },
                    new Card
                    {
                        CardId = "updateDatabase-f8a1-49e2-b7ab-2f5c6d73c93d",
                        Name = "Update Database",
                        Description = "Perform necessary updates on the database.",
                        DateOfCreation = new DateTime(2024, 2, 5, 15, 20, 0, DateTimeKind.Utc),
                        StatusId = StatusDetails.StatusNameToIdMappings[StatusDetails.InProgress],
                        AppUserId = "kev5f943-112f-4d49-888d-c671e210b8b8",
                        Color = "#32CD32" // Lime Green
                    },
                    new Card
                    {
                        CardId = "clientMeeting-2f9e-4681-a499-4a2d1b2e36e4",
                        Name = "Client Meeting",
                        Description = "Schedule a meeting with the client to discuss project updates.",
                        DateOfCreation = new DateTime(2024, 2, 10, 9, 0, 0, DateTimeKind.Utc),
                        StatusId = StatusDetails.StatusNameToIdMappings[StatusDetails.ToDo],
                        AppUserId = "kev5f943-112f-4d49-888d-c671e210b8b8",
                        Color = "#800080" // Purple
                    },
                    new Card
                    {
                        CardId = "updateDatabase-9c1f-4e7d-8737-d1f4e1ef5933",
                        Name = "Update Database",
                        Description = "Apply patches and optimize database performance.",
                        DateOfCreation = new DateTime(2024, 2, 20, 10, 15, 0, DateTimeKind.Utc),
                        StatusId = StatusDetails.StatusNameToIdMappings[StatusDetails.ToDo],
                        AppUserId = "suee8ebc-7959-4591-b86c-da19d3630419",
                        Color = "#FFA500" // Orange
                    },
                    new Card
                    {
                        CardId = "clientMeeting-7c8a-4a7d-9533-56a21b5c92e1",
                        Name = "Client Meeting",
                        Description = "Discuss project milestones and deliverables with the client.",
                        DateOfCreation = new DateTime(2024, 2, 25, 14, 30, 0, DateTimeKind.Utc),
                        StatusId = StatusDetails.StatusNameToIdMappings[StatusDetails.InProgress],
                        AppUserId = "suee8ebc-7959-4591-b86c-da19d3630419",
                        Color = "#4682B4" // Steel Blue
                    },
                    new Card
                    {
                        CardId = "clientMeeting-6c72-45fe-a7bf-4cd6d1d90c91",
                        Name = "Client Meeting",
                        Description = "Review project scope and timeline with the client.",
                        DateOfCreation = new DateTime(2024, 3, 5, 11, 0, 0, DateTimeKind.Utc),
                        StatusId = StatusDetails.StatusNameToIdMappings[StatusDetails.ToDo],
                        AppUserId = "suee8ebc-7959-4591-b86c-da19d3630419",
                        Color = "#8A2BE2" // Blue Violet
                    }


                );
        }
    }
}
