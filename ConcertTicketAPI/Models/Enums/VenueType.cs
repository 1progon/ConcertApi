using System.ComponentModel;

namespace ConcertTicketAPI.Models.Enums;

public enum VenueType
{
    None = 0,
    Arena = 1,
    Theatre = 2,
    Cinema = 3,
    Stadium = 4,
    [Description("Music Hall")] MusicHall = 5,
    Operas = 6,
    Nightclub = 7,
    Museum = 8,
    Bar = 9,
    Pub = 10,
    Club = 11,
    Restaurant = 12,
    [Description("Business Centre")] BusinessCentre = 13,
    Park = 14,
    [Description("Open Air")] OpenAir = 15,
}