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
            var category = new Category
            {
                Name = "Music",
                Description = "Music events"
            };

            var venue = new Venue
            {
                Name = "Arena Burgas",
                Address = "Main Street 1",
                City = "Burgas",
                Capacity = 500
            };

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
                new Event
                {
                    Title = "Event 1",
                    Description = "Desc 1",
                    Date = DateTime.Now,
                    Location = "Sofia",
                    CategoryId = category.Id,
                    VenueId = venue.Id
                },
                new Event
                {
                    Title = "Event 2",
                    Description = "Desc 2",
                    Date = DateTime.Now,
                    Location = "Burgas",
                    CategoryId = category.Id,
                    VenueId = venue.Id
                });

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
            Assert.AreEqual("Football Match", result.Title);
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
                new Category
                {
                    Name = "Music",
                    Description = "Music events"
                },
                new Category
                {
                    Name = "Sports",
                    Description = "Sports events"
                });

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
                new Venue
                {
                    Name = "Arena",
                    Address = "ul. 1",
                    City = "Burgas",
                    Capacity = 500
                },
                new Venue
                {
                    Name = "Stadium",
                    Address = "ul. 2",
                    City = "Sofia",
                    Capacity = 1000
                });

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
                // Title missing
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
                Date = DateTime.Now,
                Location = "Test",
                CategoryId = category.Id,
                VenueId = venue.Id
            };

            await db.Events.AddAsync(ev);
            await db.SaveChangesAsync();

            var service = new EventService(db);

            var result = await service.GetByIdAsync(ev.Id);

            Assert.IsNotNull(result.Category);
            Assert.IsNotNull(result.Venue);
        }
    }
}