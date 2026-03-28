using EventSystemManager.Data.Classes;
using EventSystemManager.Services;
using ManagerEventSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace EventSystemManager.Tests
{
    [TestClass]
    public class EventServiceTests
    {
        private ApplicationDbContext CreateInMemoryDb()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        private async Task<(Category category, Venue venue)> SeedCategoryAndVenueAsync(ApplicationDbContext db)
        {
            var category = new Category { Name = "Music", Description = "Music events" };
            var venue = new Venue { Name = "Arena Burgas", Address = "Main Street 1", City = "Burgas", Capacity = 500 };
            await db.Categories.AddAsync(category);
            await db.Venues.AddAsync(venue);
            await db.SaveChangesAsync();
            return (category, venue);
        }

        [TestMethod]
        public async Task AddAsync_AddsEventToDatabase()
        {
            var db = CreateInMemoryDb();
            var (category, venue) = await SeedCategoryAndVenueAsync(db);
            var service = new EventService(db);
            var newEvent = new Event
            {
                Title = "Test Event",
                Description = "Test Description",
                Date = DateTime.Now,
                Location = "Burgas",
                CategoryId = category.Id,
                VenueId = venue.Id
            };

            await service.AddAsync(newEvent);

            var events = await db.Events.ToListAsync();
            Assert.AreEqual(1, events.Count);
            Assert.AreEqual("Test Event", events[0].Title);
        }

        [TestMethod]
        public async Task GetAllAsync_ReturnsAllEvents()
        {
            var db = CreateInMemoryDb();
            var (category, venue) = await SeedCategoryAndVenueAsync(db);
            await db.Events.AddRangeAsync(
                new Event { Title = "Event 1", Description = "Desc 1", Date = DateTime.Now, Location = "Sofia", CategoryId = category.Id, VenueId = venue.Id },
                new Event { Title = "Event 2", Description = "Desc 2", Date = DateTime.Now, Location = "Burgas", CategoryId = category.Id, VenueId = venue.Id }
            );
            await db.SaveChangesAsync();

            var service = new EventService(db);
            var events = await service.GetAllAsync();

            Assert.AreEqual(2, events.Count);
        }

        [TestMethod]
        public async Task GetByIdAsync_ReturnsCorrectEvent()
        {
            var db = CreateInMemoryDb();
            var (category, venue) = await SeedCategoryAndVenueAsync(db);
            var newEvent = new Event
            {
                Title = "Football Match",
                Description = "Big game",
                Date = DateTime.Now,
                Location = "Burgas",
                CategoryId = category.Id,
                VenueId = venue.Id
            };
            await db.Events.AddAsync(newEvent);
            await db.SaveChangesAsync();

            var service = new EventService(db);
            var result = await service.GetByIdAsync(newEvent.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual("Football Match", result!.Title);
            Assert.AreEqual(category.Id, result.CategoryId);
            Assert.AreEqual(venue.Id, result.VenueId);
        }

        [TestMethod]
        public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
        {
            var db = CreateInMemoryDb();
            var service = new EventService(db);

            var result = await service.GetByIdAsync(999);

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task DeleteAsync_RemovesEventFromDatabase()
        {
            var db = CreateInMemoryDb();
            var (category, venue) = await SeedCategoryAndVenueAsync(db);
            var newEvent = new Event
            {
                Title = "Event to delete",
                Description = "Desc",
                Date = DateTime.Now,
                Location = "Sofia",
                CategoryId = category.Id,
                VenueId = venue.Id
            };
            await db.Events.AddAsync(newEvent);
            await db.SaveChangesAsync();

            var service = new EventService(db);
            await service.DeleteAsync(newEvent.Id);

            var events = await db.Events.ToListAsync();
            Assert.AreEqual(0, events.Count);
        }

        [TestMethod]
        public async Task DeleteAsync_DoesNothing_WhenEventNotFound()
        {
            var db = CreateInMemoryDb();
            var service = new EventService(db);

            await service.DeleteAsync(999);

            var events = await db.Events.ToListAsync();
            Assert.AreEqual(0, events.Count);
        }

        [TestMethod]
        public async Task GetAllCategoriesAsync_ReturnsAllCategories()
        {
            var db = CreateInMemoryDb();
            await db.Categories.AddRangeAsync(
                new Category { Name = "Music", Description = "Music events" },
                new Category { Name = "Sports", Description = "Sports events" }
            );
            await db.SaveChangesAsync();

            var service = new EventService(db);
            var categories = await service.GetAllCategoriesAsync();

            Assert.AreEqual(2, categories.Count);
        }

        [TestMethod]
        public async Task GetAllVenuesAsync_ReturnsAllVenues()
        {
            var db = CreateInMemoryDb();
            await db.Venues.AddRangeAsync(
                new Venue { Name = "Arena", Address = "ul. 1", City = "Burgas", Capacity = 500 },
                new Venue { Name = "Stadium", Address = "ul. 2", City = "Sofia", Capacity = 1000 }
            );
            await db.SaveChangesAsync();

            var service = new EventService(db);
            var venues = await service.GetAllVenuesAsync();

            Assert.AreEqual(2, venues.Count);
        }

        [TestMethod]
        public async Task AddAsync_Throws_WhenTitleIsMissing()
        {
            var db = CreateInMemoryDb();
            var service = new EventService(db);
            var ev = new Event
            {
                Date = DateTime.Now,
                Location = "Test",
                CategoryId = 1,
                VenueId = 1
            };

            await Assert.ThrowsExceptionAsync<DbUpdateException>(() =>
                service.AddAsync(ev));
        }

        [TestMethod]
        public async Task GetAllAsync_ReturnsEmptyList_WhenNoEvents()
        {
            var db = CreateInMemoryDb();
            var service = new EventService(db);

            var result = await service.GetAllAsync();

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task GetAllCategoriesAsync_ReturnsEmpty_WhenNoneExist()
        {
            var db = CreateInMemoryDb();
            var service = new EventService(db);

            var result = await service.GetAllCategoriesAsync();

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task GetByIdAsync_LoadsCategoryAndVenue()
        {
            var db = CreateInMemoryDb();
            var (category, venue) = await SeedCategoryAndVenueAsync(db);
            var ev = new Event
            {
                Title = "Test",
                Description = "Test desc",
                Date = DateTime.Now,
                Location = "Test",
                CategoryId = category.Id,
                VenueId = venue.Id
            };
            await db.Events.AddAsync(ev);
            await db.SaveChangesAsync();

            var service = new EventService(db);
            var result = await service.GetByIdAsync(ev.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result!.Category);
            Assert.IsNotNull(result!.Venue);
        }

        [TestClass]
        public class ParticipantServiceTests
        {
            private ApplicationDbContext CreateInMemoryDb()
            {
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;
                return new ApplicationDbContext(options);
            }

            [TestMethod]
            public async Task AddAsync_AddsParticipantToDatabase()
            {
                var db = CreateInMemoryDb();
                var service = new ParticipantService(db);
                var participant = new Participant { FirstName = "Ivan", LastName = "Ivanov", Email = "ivan@test.com" };

                await service.AddAsync(participant);

                var participants = await db.Participants.ToListAsync();
                Assert.AreEqual(1, participants.Count);
                Assert.AreEqual("Ivan", participants[0].FirstName);
            }

            [TestMethod]
            public async Task GetAllAsync_ReturnsAllParticipants()
            {
                var db = CreateInMemoryDb();
                var service = new ParticipantService(db);
                await db.Participants.AddRangeAsync(
                    new Participant { FirstName = "Ivan", LastName = "Ivanov", Email = "ivan@test.com" },
                    new Participant { FirstName = "Maria", LastName = "Petrova", Email = "maria@test.com" }
                );
                await db.SaveChangesAsync();

                var participants = await service.GetAllAsync();

                Assert.AreEqual(2, participants.Count);
            }

            [TestMethod]
            public async Task GetAllAsync_ReturnsEmpty_WhenNoneExist()
            {
                var db = CreateInMemoryDb();
                var service = new ParticipantService(db);

                var result = await service.GetAllAsync();

                Assert.AreEqual(0, result.Count);
            }

            [TestMethod]
            public async Task DeleteAsync_RemovesParticipantFromDatabase()
            {
                var db = CreateInMemoryDb();
                var service = new ParticipantService(db);
                var participant = new Participant { FirstName = "Ivan", LastName = "Ivanov", Email = "ivan@test.com" };
                await db.Participants.AddAsync(participant);
                await db.SaveChangesAsync();

                await service.DeleteAsync(participant.Id);

                var participants = await db.Participants.ToListAsync();
                Assert.AreEqual(0, participants.Count);
            }

            [TestMethod]
            public async Task DeleteAsync_DoesNothing_WhenNotFound()
            {
                var db = CreateInMemoryDb();
                var service = new ParticipantService(db);

                await service.DeleteAsync(999);

                var participants = await db.Participants.ToListAsync();
                Assert.AreEqual(0, participants.Count);
            }

            [TestMethod]
            public async Task AddAsync_CorrectlySavesAllFields()
            {
                var db = CreateInMemoryDb();
                var service = new ParticipantService(db);
                var participant = new Participant
                {
                    FirstName = "Ivan",
                    LastName = "Ivanov",
                    Email = "ivan@test.com",
                    Phone = "0888123456"
                };

                await service.AddAsync(participant);

                var result = await db.Participants.FindAsync(participant.Id);
                Assert.IsNotNull(result);
                Assert.AreEqual("Ivan", result!.FirstName);
                Assert.AreEqual("Ivanov", result.LastName);
                Assert.AreEqual("ivan@test.com", result.Email);
                Assert.AreEqual("0888123456", result.Phone);
            }
        }

        [TestClass]
        public class RegistrationServiceTests
        {
            private ApplicationDbContext CreateInMemoryDb()
            {
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;
                return new ApplicationDbContext(options);
            }

            [TestMethod]
            public async Task AddAsync_AddsRegistrationToDatabase()
            {
                var db = CreateInMemoryDb();
                var service = new RegistrationService(db);
                var ev = new Event { Title = "Event", Description = "Desc", Date = DateTime.Now, Location = "Burgas" };
                var participant = new Participant { FirstName = "Ivan", LastName = "Ivanov", Email = "ivan@test.com" };
                await db.Events.AddAsync(ev);
                await db.Participants.AddAsync(participant);
                await db.SaveChangesAsync();

                var registration = new Registration { EventId = ev.Id, ParticipantId = participant.Id, RegisteredOn = DateTime.UtcNow };

                await service.AddAsync(registration);

                var registrations = await db.Registrations.ToListAsync();
                Assert.AreEqual(1, registrations.Count);
            }

            [TestMethod]
            public async Task DeleteAsync_RemovesRegistrationFromDatabase()
            {
                var db = CreateInMemoryDb();
                var service = new RegistrationService(db);
                var ev = new Event { Title = "Event", Description = "Desc", Date = DateTime.Now, Location = "Burgas" };
                var participant = new Participant { FirstName = "Ivan", LastName = "Ivanov", Email = "ivan@test.com" };
                await db.Events.AddAsync(ev);
                await db.Participants.AddAsync(participant);
                await db.SaveChangesAsync();

                var registration = new Registration { EventId = ev.Id, ParticipantId = participant.Id, RegisteredOn = DateTime.UtcNow };
                await db.Registrations.AddAsync(registration);
                await db.SaveChangesAsync();

                await service.DeleteAsync(registration.Id);

                var registrations = await db.Registrations.ToListAsync();
                Assert.AreEqual(0, registrations.Count);
            }

            [TestMethod]
            public async Task GetAllAsync_ReturnsAllRegistrations()
            {
                var db = CreateInMemoryDb();
                var service = new RegistrationService(db);
                var ev = new Event { Title = "Event", Description = "Desc", Date = DateTime.Now, Location = "Burgas" };
                var participant = new Participant { FirstName = "Ivan", LastName = "Ivanov", Email = "ivan@test.com" };
                await db.Events.AddAsync(ev);
                await db.Participants.AddAsync(participant);
                await db.SaveChangesAsync();

                await db.Registrations.AddRangeAsync(
                    new Registration { EventId = ev.Id, ParticipantId = participant.Id, RegisteredOn = DateTime.UtcNow },
                    new Registration { EventId = ev.Id, ParticipantId = participant.Id, RegisteredOn = DateTime.UtcNow }
                );
                await db.SaveChangesAsync();

                var registrations = await service.GetAllAsync();
                Assert.AreEqual(2, registrations.Count);
            }

            [TestMethod]
            public async Task GetAllAsync_ReturnsEmpty_WhenNoneExist()
            {
                var db = CreateInMemoryDb();
                var service = new RegistrationService(db);

                var result = await service.GetAllAsync();

                Assert.AreEqual(0, result.Count);
            }

            [TestMethod]
            public async Task DeleteAsync_DoesNothing_WhenNotFound()
            {
                var db = CreateInMemoryDb();
                var service = new RegistrationService(db);

                await service.DeleteAsync(999);

                var registrations = await db.Registrations.ToListAsync();
                Assert.AreEqual(0, registrations.Count);
            }

            [TestMethod]
            public async Task GetByIdAsync_ReturnsCorrectRegistration()
            {
                var db = CreateInMemoryDb();
                var service = new RegistrationService(db);
                var ev = new Event { Title = "Event", Description = "Desc", Date = DateTime.Now, Location = "Burgas" };
                var participant = new Participant { FirstName = "Ivan", LastName = "Ivanov", Email = "ivan@test.com" };
                await db.Events.AddAsync(ev);
                await db.Participants.AddAsync(participant);
                await db.SaveChangesAsync();

                var registration = new Registration { EventId = ev.Id, ParticipantId = participant.Id, RegisteredOn = DateTime.UtcNow };
                await db.Registrations.AddAsync(registration);
                await db.SaveChangesAsync();

                var result = await service.GetByIdAsync(registration.Id);

                Assert.IsNotNull(result);
                Assert.AreEqual(ev.Id, result!.EventId);
                Assert.AreEqual(participant.Id, result.ParticipantId);
            }

            [TestMethod]
            public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
            {
                var db = CreateInMemoryDb();
                var service = new RegistrationService(db);

                var result = await service.GetByIdAsync(999);

                Assert.IsNull(result);
            }
        }

        [TestClass]
        public class CategoryServiceTests
        {
            private ApplicationDbContext CreateInMemoryDb()
            {
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;
                return new ApplicationDbContext(options);
            }

            [TestMethod]
            public async Task AddAsync_AddsCategoryToDatabase()
            {
                var db = CreateInMemoryDb();
                var service = new CategoryService(db);
                var category = new Category { Name = "Music", Description = "Music events" };

                await service.AddAsync(category);

                var categories = await db.Categories.ToListAsync();
                Assert.AreEqual(1, categories.Count);
                Assert.AreEqual("Music", categories[0].Name);
            }

            [TestMethod]
            public async Task GetAllAsync_ReturnsAllCategories()
            {
                var db = CreateInMemoryDb();
                var service = new CategoryService(db);
                await db.Categories.AddRangeAsync(
                    new Category { Name = "Music", Description = "Music events" },
                    new Category { Name = "Sports", Description = "Sports events" },
                    new Category { Name = "Tech", Description = "Tech events" }
                );
                await db.SaveChangesAsync();

                var categories = await service.GetAllAsync();

                Assert.AreEqual(3, categories.Count);
            }

            [TestMethod]
            public async Task GetAllAsync_ReturnsEmpty_WhenNoneExist()
            {
                var db = CreateInMemoryDb();
                var service = new CategoryService(db);

                var result = await service.GetAllAsync();

                Assert.AreEqual(0, result.Count);
            }

            [TestMethod]
            public async Task GetByIdAsync_ReturnsCorrectCategory()
            {
                var db = CreateInMemoryDb();
                var service = new CategoryService(db);
                var category = new Category { Name = "Music", Description = "Music events" };
                await db.Categories.AddAsync(category);
                await db.SaveChangesAsync();

                var result = await service.GetByIdAsync(category.Id);

                Assert.IsNotNull(result);
                Assert.AreEqual("Music", result!.Name);
            }

            [TestMethod]
            public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
            {
                var db = CreateInMemoryDb();
                var service = new CategoryService(db);

                var result = await service.GetByIdAsync(999);

                Assert.IsNull(result);
            }

            [TestMethod]
            public async Task DeleteAsync_RemovesCategoryFromDatabase()
            {
                var db = CreateInMemoryDb();
                var service = new CategoryService(db);
                var category = new Category { Name = "Music", Description = "Music events" };
                await db.Categories.AddAsync(category);
                await db.SaveChangesAsync();

                await service.DeleteAsync(category.Id);

                var categories = await db.Categories.ToListAsync();
                Assert.AreEqual(0, categories.Count);
            }

            [TestMethod]
            public async Task DeleteAsync_DoesNothing_WhenNotFound()
            {
                var db = CreateInMemoryDb();
                var service = new CategoryService(db);

                await service.DeleteAsync(999);

                var categories = await db.Categories.ToListAsync();
                Assert.AreEqual(0, categories.Count);
            }

            [TestMethod]
            public async Task UpdateAsync_UpdatesCategoryInDatabase()
            {
                var db = CreateInMemoryDb();
                var service = new CategoryService(db);
                var category = new Category { Name = "Music", Description = "Old description" };
                await db.Categories.AddAsync(category);
                await db.SaveChangesAsync();

                category.Description = "New description";
                await service.UpdateAsync(category);

                var result = await db.Categories.FindAsync(category.Id);
                Assert.AreEqual("New description", result!.Description);
            }
        }

        [TestClass]
        public class VenueServiceTests
        {
            private ApplicationDbContext CreateInMemoryDb()
            {
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;
                return new ApplicationDbContext(options);
            }

            [TestMethod]
            public async Task AddAsync_AddsVenueToDatabase()
            {
                var db = CreateInMemoryDb();
                var service = new VenueService(db);
                var venue = new Venue { Name = "Arena", Address = "ul. 1", City = "Burgas", Capacity = 500 };

                await service.AddAsync(venue);

                var venues = await db.Venues.ToListAsync();
                Assert.AreEqual(1, venues.Count);
                Assert.AreEqual("Arena", venues[0].Name);
            }

            [TestMethod]
            public async Task GetAllAsync_ReturnsAllVenues()
            {
                var db = CreateInMemoryDb();
                var service = new VenueService(db);
                await db.Venues.AddRangeAsync(
                    new Venue { Name = "Arena", Address = "ul. 1", City = "Burgas", Capacity = 500 },
                    new Venue { Name = "Stadium", Address = "ul. 2", City = "Sofia", Capacity = 1000 },
                    new Venue { Name = "Hall", Address = "ul. 3", City = "Plovdiv", Capacity = 200 }
                );
                await db.SaveChangesAsync();

                var venues = await service.GetAllAsync();

                Assert.AreEqual(3, venues.Count);
            }

            [TestMethod]
            public async Task GetAllAsync_ReturnsEmpty_WhenNoneExist()
            {
                var db = CreateInMemoryDb();
                var service = new VenueService(db);

                var venues = await service.GetAllAsync();

                Assert.AreEqual(0, venues.Count);
            }

            [TestMethod]
            public async Task DeleteAsync_RemovesVenueFromDatabase()
            {
                var db = CreateInMemoryDb();
                var service = new VenueService(db);
                var venue = new Venue { Name = "Arena", Address = "ul. 1", City = "Burgas", Capacity = 500 };
                await db.Venues.AddAsync(venue);
                await db.SaveChangesAsync();

                await service.DeleteAsync(venue.Id);

                var venues = await db.Venues.ToListAsync();
                Assert.AreEqual(0, venues.Count);
            }

            [TestMethod]
            public async Task DeleteAsync_DoesNothing_WhenNotFound()
            {
                var db = CreateInMemoryDb();
                var service = new VenueService(db);

                await service.DeleteAsync(999);

                var venues = await db.Venues.ToListAsync();
                Assert.AreEqual(0, venues.Count);
            }

            [TestMethod]
            public async Task AddAsync_CorrectlySavesAllFields()
            {
                var db = CreateInMemoryDb();
                var service = new VenueService(db);
                var venue = new Venue
                {
                    Name = "Arena Burgas",
                    Address = "Main Street 1",
                    City = "Burgas",
                    Capacity = 5000
                };

                await service.AddAsync(venue);

                var result = await db.Venues.FindAsync(venue.Id);
                Assert.IsNotNull(result);
                Assert.AreEqual("Arena Burgas", result!.Name);
                Assert.AreEqual("Main Street 1", result.Address);
                Assert.AreEqual("Burgas", result.City);
                Assert.AreEqual(5000, result.Capacity);
            }
        }
    }


    [TestClass]
    public class ParticipantModelTests
    {
        [TestMethod]
        public void GetFullName_ReturnsCorrectFullName()
        {
            // Arrange
            var participant = new Participant
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                Email = "ivan@test.com"
            };

            // Act
            var fullName = participant.GetFullName();

            // Assert
            Assert.AreEqual("Ivan Ivanov", fullName);
        }

        [TestMethod]
        public void GetFullName_WithDifferentNames_ReturnsCorrectFullName()
        {
            // Arrange
            var participant = new Participant
            {
                FirstName = "Maria",
                LastName = "Petrova",
                Email = "maria@test.com"
            };

            // Act
            var fullName = participant.GetFullName();

            // Assert
            Assert.AreEqual("Maria Petrova", fullName);
        }

        [TestMethod]
        public void GetFullName_CombinesFirstAndLastNameWithSpace()
        {
            // Arrange
            var participant = new Participant
            {
                FirstName = "Dimitar",
                LastName = "Dimitrov",
                Email = "dimitar@test.com"
            };

            // Act
            var result = participant.GetFullName();

            // Assert
            Assert.IsTrue(result.Contains(" "));
            Assert.IsTrue(result.StartsWith("Dimitar"));
            Assert.IsTrue(result.EndsWith("Dimitrov"));
        }
    }

    [TestClass]
    public class EventModelTests
    {
        [TestMethod]
        public void Event_Date_CanBeSetInFuture()
        {
            // Arrange
            var futureDate = DateTime.Now.AddDays(30);
            var ev = new Event
            {
                Title = "Future Event",
                Date = futureDate,
                Location = "Burgas"
            };

            // Act & Assert
            Assert.AreEqual(futureDate, ev.Date);
        }

        [TestMethod]
        public void Event_Date_CanBeSetInPast()
        {
            // Arrange
            var pastDate = DateTime.Now.AddDays(-10);
            var ev = new Event
            {
                Title = "Past Event",
                Date = pastDate,
                Location = "Sofia"
            };

            // Act & Assert
            Assert.AreEqual(pastDate, ev.Date);
        }

        [TestMethod]
        public void Event_Date_IsCorrectlyStored()
        {
            // Arrange
            var specificDate = new DateTime(2026, 06, 15, 18, 0, 0);
            var ev = new Event
            {
                Title = "Concert",
                Date = specificDate,
                Location = "Burgas"
            };

            // Act & Assert
            Assert.AreEqual(2026, ev.Date.Year);
            Assert.AreEqual(6, ev.Date.Month);
            Assert.AreEqual(15, ev.Date.Day);
            Assert.AreEqual(18, ev.Date.Hour);
        }

        [TestMethod]
        public void Event_ImageUrl_IsNullByDefault()
        {
            // Arrange
            var ev = new Event
            {
                Title = "Test",
                Date = DateTime.Now,
                Location = "Burgas"
            };

            // Assert
            Assert.IsNull(ev.ImageUrl);
        }

        [TestMethod]
        public void Event_ImageUrl_CanBeSet()
        {
            // Arrange
            var ev = new Event
            {
                Title = "Test",
                Date = DateTime.Now,
                Location = "Burgas",
                ImageUrl = "/images/events/test.jpg"
            };

            // Assert
            Assert.AreEqual("/images/events/test.jpg", ev.ImageUrl);
        }
    }

    [TestClass]
    public class VenueModelTests
    {
        [TestMethod]
        public void Venue_Capacity_CanBeSet()
        {
            // Arrange
            var venue = new Venue
            {
                Name = "Arena",
                Address = "ul. 1",
                City = "Burgas",
                Capacity = 500
            };

            // Act & Assert
            Assert.AreEqual(500, venue.Capacity);
        }

        [TestMethod]
        public void Venue_Capacity_CanBeZero()
        {
            // Arrange
            var venue = new Venue
            {
                Name = "Small Room",
                Address = "ul. 2",
                City = "Sofia",
                Capacity = 0
            };

            // Assert
            Assert.AreEqual(0, venue.Capacity);
        }

        [TestMethod]
        public void Venue_Capacity_CanBeLarge()
        {
            // Arrange
            var venue = new Venue
            {
                Name = "Stadium",
                Address = "ul. 3",
                City = "Sofia",
                Capacity = 50000
            };

            // Assert
            Assert.AreEqual(50000, venue.Capacity);
        }

        [TestMethod]
        public void Venue_Events_IsEmptyByDefault()
        {
            // Arrange
            var venue = new Venue
            {
                Name = "Arena",
                Address = "ul. 1",
                City = "Burgas",
                Capacity = 500
            };

            // Assert
            Assert.IsNotNull(venue.Events);
            Assert.AreEqual(0, venue.Events.Count);
        }
    }

    [TestClass]
    public class CategoryModelTests
    {
        [TestMethod]
        public void Category_Events_IsEmptyByDefault()
        {
            // Arrange
            var category = new Category
            {
                Name = "Music",
                Description = "Music events"
            };

            // Assert
            Assert.IsNotNull(category.Events);
            Assert.AreEqual(0, category.Events.Count);
        }

        [TestMethod]
        public void Category_Events_CanAddEvents()
        {
            // Arrange
            var category = new Category
            {
                Name = "Music",
                Description = "Music events"
            };

            var ev = new Event
            {
                Title = "Concert",
                Date = DateTime.Now,
                Location = "Burgas"
            };

            // Act
            category.Events.Add(ev);

            // Assert
            Assert.AreEqual(1, category.Events.Count);
            Assert.AreEqual("Concert", category.Events[0].Title);
        }

        [TestMethod]
        public void Category_Events_CanAddMultipleEvents()
        {
            // Arrange
            var category = new Category
            {
                Name = "Sports",
                Description = "Sports events"
            };

            // Act
            category.Events.Add(new Event { Title = "Football", Date = DateTime.Now, Location = "Sofia" });
            category.Events.Add(new Event { Title = "Basketball", Date = DateTime.Now, Location = "Burgas" });
            category.Events.Add(new Event { Title = "Tennis", Date = DateTime.Now, Location = "Plovdiv" });

            // Assert
            Assert.AreEqual(3, category.Events.Count);
        }

        [TestMethod]
        public void Category_Name_CanBeSet()
        {
            // Arrange
            var category = new Category
            {
                Name = "Technology",
                Description = "Tech events"
            };

            // Assert
            Assert.AreEqual("Technology", category.Name);
            Assert.AreEqual("Tech events", category.Description);
        }
    }
    [TestClass]
    public class AdditionalParticipantServiceTests
{
    private ApplicationDbContext CreateInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new ApplicationDbContext(options);
    }

    [TestMethod]
    public async Task GetByIdAsync_ReturnsCorrectParticipant()
    {
        var db = CreateInMemoryDb();
        var service = new ParticipantService(db);
        var participant = new Participant
        {
            FirstName = "Georgi",
            LastName = "Georgiev",
            Email = "georgi@test.com",
            Phone = "0899999999"
        };

        await db.Participants.AddAsync(participant);
        await db.SaveChangesAsync();

        var result = await service.GetByIdAsync(participant.Id);

        Assert.IsNotNull(result);
        Assert.AreEqual("Georgi", result!.FirstName);
        Assert.AreEqual("Georgiev", result.LastName);
        Assert.AreEqual("georgi@test.com", result.Email);
    }

    [TestMethod]
    public async Task GetByIdAsync_ReturnsNull_WhenParticipantNotFound()
    {
        var db = CreateInMemoryDb();
        var service = new ParticipantService(db);

        var result = await service.GetByIdAsync(999);

        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task AddAsync_AddsMultipleParticipantsToDatabase()
    {
        var db = CreateInMemoryDb();
        var service = new ParticipantService(db);

        await service.AddAsync(new Participant
        {
            FirstName = "Ivan",
            LastName = "Ivanov",
            Email = "ivan@test.com"
        });

        await service.AddAsync(new Participant
        {
            FirstName = "Maria",
            LastName = "Petrova",
            Email = "maria@test.com"
        });

        var participants = await db.Participants.ToListAsync();

        Assert.AreEqual(2, participants.Count);
    }

    [TestMethod]
    public async Task DeleteAsync_RemovesOnlySelectedParticipant()
    {
        var db = CreateInMemoryDb();
        var service = new ParticipantService(db);

        var firstParticipant = new Participant
        {
            FirstName = "Ivan",
            LastName = "Ivanov",
            Email = "ivan@test.com"
        };

        var secondParticipant = new Participant
        {
            FirstName = "Maria",
            LastName = "Petrova",
            Email = "maria@test.com"
        };

        await db.Participants.AddRangeAsync(firstParticipant, secondParticipant);
        await db.SaveChangesAsync();

        await service.DeleteAsync(firstParticipant.Id);

        var participants = await db.Participants.ToListAsync();

        Assert.AreEqual(1, participants.Count);
        Assert.AreEqual("Maria", participants[0].FirstName);
    }

    [TestMethod]
    public void Participant_ToString_ReturnsExpectedFormat()
    {
        var participant = new Participant
        {
            Id = 1,
            FirstName = "Ivan",
            LastName = "Ivanov",
            Email = "ivan@test.com"
        };

        var result = participant.ToString();

        Assert.IsTrue(result.Contains("Ivan Ivanov"));
        Assert.IsTrue(result.Contains("ivan@test.com"));
        Assert.IsTrue(result.Contains("1"));
    }
}
    [TestClass]
public class AdditionalRegistrationTests
{
    private ApplicationDbContext CreateInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new ApplicationDbContext(options);
    }

    [TestMethod]
    public async Task AddAsync_SavesCorrectRegisteredOnDate()
    {
        var db = CreateInMemoryDb();
        var service = new RegistrationService(db);

        var ev = new Event
        {
            Title = "Conference",
            Description = "Business event",
            Date = DateTime.Now,
            Location = "Burgas"
        };

        var participant = new Participant
        {
            FirstName = "Ivan",
            LastName = "Ivanov",
            Email = "ivan@test.com"
        };

        await db.Events.AddAsync(ev);
        await db.Participants.AddAsync(participant);
        await db.SaveChangesAsync();

        var registeredOn = new DateTime(2026, 5, 10, 14, 30, 0);
        var registration = new Registration
        {
            EventId = ev.Id,
            ParticipantId = participant.Id,
            RegisteredOn = registeredOn
        };

        await service.AddAsync(registration);

        var result = await db.Registrations.FirstAsync();

        Assert.AreEqual(2026, result.RegisteredOn.Year);
        Assert.AreEqual(5, result.RegisteredOn.Month);
        Assert.AreEqual(10, result.RegisteredOn.Day);
        Assert.AreEqual(14, result.RegisteredOn.Hour);
        Assert.AreEqual(30, result.RegisteredOn.Minute);
    }

    [TestMethod]
    public async Task GetByIdAsync_ReturnsRegistrationWithCorrectRegisteredOn()
    {
        var db = CreateInMemoryDb();
        var service = new RegistrationService(db);

        var ev = new Event
        {
            Title = "Workshop",
            Description = "Practical training",
            Date = DateTime.Now,
            Location = "Sofia"
        };

        var participant = new Participant
        {
            FirstName = "Maria",
            LastName = "Petrova",
            Email = "maria@test.com"
        };

        await db.Events.AddAsync(ev);
        await db.Participants.AddAsync(participant);
        await db.SaveChangesAsync();

        var registration = new Registration
        {
            EventId = ev.Id,
            ParticipantId = participant.Id,
            RegisteredOn = new DateTime(2026, 6, 1)
        };

        await db.Registrations.AddAsync(registration);
        await db.SaveChangesAsync();

        var result = await service.GetByIdAsync(registration.Id);

        Assert.IsNotNull(result);
        Assert.AreEqual(new DateTime(2026, 6, 1), result!.RegisteredOn);
    }

    [TestMethod]
    public async Task DeleteAsync_DeletesCorrectRegistration_WhenThereAreMultiple()
    {
        var db = CreateInMemoryDb();
        var service = new RegistrationService(db);

        var ev1 = new Event
        {
            Title = "Event 1",
            Description = "Desc",
            Date = DateTime.Now,
            Location = "Burgas"
        };

        var ev2 = new Event
        {
            Title = "Event 2",
            Description = "Desc",
            Date = DateTime.Now,
            Location = "Varna"
        };

        var participant1 = new Participant
        {
            FirstName = "Ivan",
            LastName = "Ivanov",
            Email = "ivan@test.com"
        };

        var participant2 = new Participant
        {
            FirstName = "Maria",
            LastName = "Petrova",
            Email = "maria@test.com"
        };

        await db.Events.AddRangeAsync(ev1, ev2);
        await db.Participants.AddRangeAsync(participant1, participant2);
        await db.SaveChangesAsync();

        var registration1 = new Registration
        {
            EventId = ev1.Id,
            ParticipantId = participant1.Id,
            RegisteredOn = DateTime.UtcNow
        };

        var registration2 = new Registration
        {
            EventId = ev2.Id,
            ParticipantId = participant2.Id,
            RegisteredOn = DateTime.UtcNow
        };

        await db.Registrations.AddRangeAsync(registration1, registration2);
        await db.SaveChangesAsync();

        await service.DeleteAsync(registration1.Id);

        var registrations = await db.Registrations.ToListAsync();

        Assert.AreEqual(1, registrations.Count);
        Assert.AreEqual(registration2.Id, registrations[0].Id);
    }

    [TestMethod]
    public void Registration_RegisteredOn_CanBeSetCorrectly()
    {
        var date = new DateTime(2026, 7, 20, 9, 15, 0);
        var registration = new Registration
        {
            EventId = 1,
            ParticipantId = 1,
            RegisteredOn = date
        };

        Assert.AreEqual(date, registration.RegisteredOn);
    }

    [TestMethod]
    public async Task GetAllAsync_ReturnsRegistrationsForDifferentEventsAndParticipants()
    {
        var db = CreateInMemoryDb();
        var service = new RegistrationService(db);

        var ev1 = new Event
        {
            Title = "Concert",
            Description = "Music",
            Date = DateTime.Now,
            Location = "Burgas"
        };

        var ev2 = new Event
        {
            Title = "Seminar",
            Description = "Education",
            Date = DateTime.Now,
            Location = "Sofia"
        };

        var participant1 = new Participant
        {
            FirstName = "Petar",
            LastName = "Petrov",
            Email = "petar@test.com"
        };

        var participant2 = new Participant
        {
            FirstName = "Elena",
            LastName = "Ivanova",
            Email = "elena@test.com"
        };

        await db.Events.AddRangeAsync(ev1, ev2);
        await db.Participants.AddRangeAsync(participant1, participant2);
        await db.SaveChangesAsync();

        await db.Registrations.AddRangeAsync(
            new Registration
            {
                EventId = ev1.Id,
                ParticipantId = participant1.Id,
                RegisteredOn = DateTime.UtcNow
            },
            new Registration
            {
                EventId = ev2.Id,
                ParticipantId = participant2.Id,
                RegisteredOn = DateTime.UtcNow
            });

        await db.SaveChangesAsync();

        var result = await service.GetAllAsync();

        Assert.AreEqual(2, result.Count);
        Assert.IsTrue(result.Any(r => r.EventId == ev1.Id && r.ParticipantId == participant1.Id));
        Assert.IsTrue(result.Any(r => r.EventId == ev2.Id && r.ParticipantId == participant2.Id));
    }
}
}
