namespace SexyFishHorse.CitiesSkylines.Birdcage.Helpers
{
    using System.Collections.Generic;

    /// <remarks>
    /// How to generate a new list:
    ///
    /// 1. Copy the properties (and their attributes) from <see cref="LocaleID"/> into Notepad++
    /// 2. Extract and convert property+attribute to a single formatted line
    ///     Search & replace regex:
    ///         Search: <code>\h*\[[\w\.]+\(category = "(.*)", file = .+\r?\n\s+public const string (.+) = \".+\";</code>
    ///         Replace: <code>[$1] LocaleID.$2;</code>
    /// 3. Remove all lines that does not contain "chirp"
    ///     Search & replace regex: <code>^((?!chirp).)*$\r?\n?</code> (empty replace field)
    /// 4. Sort lines for ease of reading
    ///     Edit -> Line Operations -> Sort lines Lex. Ascending (either works)
    /// 5. Remove any lines that aren't chirper lines
    /// 6. Remove category prefix once done
    ///     Search & replace regex:
    ///         Search: <code>.+ {</code>
    ///         Replace: <code>{</code>
    ///
    /// Note that CHIRP_DEFAULT is not filtered (probably set up directly in the prefab instead of fed in like the other chirps)
    ///</remarks>
    public static class Chirps
    {
        public static readonly HashSet<string> FirstTypeOfServiceBuilt = new HashSet<string>
        {
            LocaleID.CHIRP_FIRST_ACADEMIC_LIBRARY,
            LocaleID.CHIRP_FIRST_ADVANCED_WATER_TREATMENT_PLANT,
            LocaleID.CHIRP_FIRST_AIRLINELOUNGE,
            LocaleID.CHIRP_FIRST_AIRPORT,
            LocaleID.CHIRP_FIRST_AIRPORTHOTEL,
            LocaleID.CHIRP_FIRST_AIRPORTHOTEL_LARGE,
            LocaleID.CHIRP_FIRST_AIRPORTMETRO,
            LocaleID.CHIRP_FIRST_AMERICAN_FOOTBALL_STADIUM,
            LocaleID.CHIRP_FIRST_AMUSEMENT_PARK_MAIN_GATE,
            LocaleID.CHIRP_FIRST_AMUSEMENT_PARK_RESTROOMS_01,
            LocaleID.CHIRP_FIRST_AMUSEMENT_PARK_SIDE_GATE,
            LocaleID.CHIRP_FIRST_AMUSEMENT_PARK_SOUVENIR_SHOP_01,
            LocaleID.CHIRP_FIRST_ANIMAL_PASTURE_01,
            LocaleID.CHIRP_FIRST_ANIMAL_PASTURE_02,
            LocaleID.CHIRP_FIRST_ANTELOPE_ENCLOSURE,
            LocaleID.CHIRP_FIRST_AQUATICS_CENTER,
            LocaleID.CHIRP_FIRST_AVIATIONMUSEUM,
            LocaleID.CHIRP_FIRST_AVIATION_CLUB,
            LocaleID.CHIRP_FIRST_BAKERY,
            LocaleID.CHIRP_FIRST_BARN_01,
            LocaleID.CHIRP_FIRST_BARN_02,
            LocaleID.CHIRP_FIRST_BASEBALL_PARK,
            LocaleID.CHIRP_FIRST_BASKETBALL_ARENA,
            LocaleID.CHIRP_FIRST_BIOFUEL_BUS_DEPOT,
            LocaleID.CHIRP_FIRST_BIOMASS_PELLET_PLANT,
            LocaleID.CHIRP_FIRST_BIRD_AND_BEE_HAVEN,
            LocaleID.CHIRP_FIRST_BIRD_HOUSE,
            LocaleID.CHIRP_FIRST_BISON_ENCLOSURE,
            LocaleID.CHIRP_FIRST_BLIMPLINE,
            LocaleID.CHIRP_FIRST_BOATMUSEUM,
            LocaleID.CHIRP_FIRST_BOILERSTATION,
            LocaleID.CHIRP_FIRST_BOULDERING_SITE_01,
            LocaleID.CHIRP_FIRST_BUMPER_CARS,
            LocaleID.CHIRP_FIRST_BUSTATION,
            LocaleID.CHIRP_FIRST_BUS_DEPOT,
            LocaleID.CHIRP_FIRST_BUS_LINE,
            LocaleID.CHIRP_FIRST_CABLECARLINE,
            LocaleID.CHIRP_FIRST_CAMPFIRE_SITE_01,
            LocaleID.CHIRP_FIRST_CAMPFIRE_SITE_02,
            LocaleID.CHIRP_FIRST_CAMPING_SITE_01,
            LocaleID.CHIRP_FIRST_CARGOHUB,
            LocaleID.CHIRP_FIRST_CARGO_AIRPORT,
            LocaleID.CHIRP_FIRST_CARGO_AIRPORT_HUB,
            LocaleID.CHIRP_FIRST_CARGO_CENTER,
            LocaleID.CHIRP_FIRST_CARGO_HARBOR,
            LocaleID.CHIRP_FIRST_CAROUSEL,
            LocaleID.CHIRP_FIRST_CAR_FACTORY,
            LocaleID.CHIRP_FIRST_CASTLE_OF_LORD_CHIRPWICK,
            LocaleID.CHIRP_FIRST_CATTLE_SHED,
            LocaleID.CHIRP_FIRST_CEMETERY,
            LocaleID.CHIRP_FIRST_CENTRAL_PARK,
            LocaleID.CHIRP_FIRST_CHILD_HEALTH_CENTER,
            LocaleID.CHIRP_FIRST_CITY_ARCH,
            LocaleID.CHIRP_FIRST_CITY_HALL,
            LocaleID.CHIRP_FIRST_CLIMATE_RESEARCH_STATION,
            LocaleID.CHIRP_FIRST_CLIMBING_FRAME_01,
            LocaleID.CHIRP_FIRST_CLINIC,
            LocaleID.CHIRP_FIRST_CLOCK_TOWER,
            LocaleID.CHIRP_FIRST_CLOTHING_FACTORY,
            LocaleID.CHIRP_FIRST_COAL_OR_OIL_PLANT,
            LocaleID.CHIRP_FIRST_COMBUSTION_PLANT,
            LocaleID.CHIRP_FIRST_COMMUNITY_POOL,
            LocaleID.CHIRP_FIRST_COMMUNITY_POOL_WINTER,
            LocaleID.CHIRP_FIRST_COMMUNITY_SCHOOL,
            LocaleID.CHIRP_FIRST_CONCOURSEHUB_STYLE_A,
            LocaleID.CHIRP_FIRST_CREMATORY,
            LocaleID.CHIRP_FIRST_CROP_FIELD_01,
            LocaleID.CHIRP_FIRST_CROP_FIELD_02,
            LocaleID.CHIRP_FIRST_CROP_FIELD_03,
            LocaleID.CHIRP_FIRST_CROP_FIELD_GREENHOUSE_01,
            LocaleID.CHIRP_FIRST_CROP_FIELD_GREENHOUSE_02,
            LocaleID.CHIRP_FIRST_CROP_FIELD_GREENHOUSE_03,
            LocaleID.CHIRP_FIRST_CRUDE_OIL_STORAGE_CAVERN,
            LocaleID.CHIRP_FIRST_CRUDE_OIL_TANK_FARM_01,
            LocaleID.CHIRP_FIRST_CRUDE_OIL_TANK_FARM_02,
            LocaleID.CHIRP_FIRST_DISASTERMEMORIAL,
            LocaleID.CHIRP_FIRST_DISASTERRESPONSE,
            LocaleID.CHIRP_FIRST_DOOMSDAYVAULT,
            LocaleID.CHIRP_FIRST_DRAIN_PIPE,
            LocaleID.CHIRP_FIRST_DROP_TOWER_RIDE,
            LocaleID.CHIRP_FIRST_DRY_DOCK,
            LocaleID.CHIRP_FIRST_EARTHQUAKESENSOR,
            LocaleID.CHIRP_FIRST_ECO_ADVANCED_INLAND_WATER_TREATMENT_PLANT,
            LocaleID.CHIRP_FIRST_ECO_INLAND_WATER_TREATMENT_PLANT,
            LocaleID.CHIRP_FIRST_ECO_WATER_OUTLET,
            LocaleID.CHIRP_FIRST_ECO_WATER_TREATMENT_PLANT,
            LocaleID.CHIRP_FIRST_ELDERCARE,
            LocaleID.CHIRP_FIRST_ELECTRONICS_FACTORY,
            LocaleID.CHIRP_FIRST_ELEMENTARY_SCHOOL,
            LocaleID.CHIRP_FIRST_ELEPHANT_ENCLOSURE,
            LocaleID.CHIRP_FIRST_ENDOFLINETRAINSTATION,
            LocaleID.CHIRP_FIRST_ENGINEERED_WOOD_PLANT,
            LocaleID.CHIRP_FIRST_FARM_MAINTENANCE_BUILDING,
            LocaleID.CHIRP_FIRST_FARM_MAIN_BUILDING_LEVEL_01,
            LocaleID.CHIRP_FIRST_FARM_MAIN_BUILDING_LEVEL_02,
            LocaleID.CHIRP_FIRST_FARM_MAIN_BUILDING_LEVEL_03,
            LocaleID.CHIRP_FIRST_FARM_WORKERS_BARRACKS,
            LocaleID.CHIRP_FIRST_FERRIS_WHEEL,
            LocaleID.CHIRP_FIRST_FERRYLINE,
            LocaleID.CHIRP_FIRST_FIBERGLASS_PLANT,
            LocaleID.CHIRP_FIRST_FIREHELI,
            LocaleID.CHIRP_FIRST_FIREWATCH,
            LocaleID.CHIRP_FIRST_FIRE_HOUSE,
            LocaleID.CHIRP_FIRST_FIRE_STATION,
            LocaleID.CHIRP_FIRST_FISHING_BOAT_HARBOR_01,
            LocaleID.CHIRP_FIRST_FISHING_BOAT_HARBOR_02,
            LocaleID.CHIRP_FIRST_FISHING_BOAT_HARBOR_03,
            LocaleID.CHIRP_FIRST_FISHING_BOAT_HARBOR_04,
            LocaleID.CHIRP_FIRST_FISHING_BOAT_HARBOR_05,
            LocaleID.CHIRP_FIRST_FISHING_CABIN_01,
            LocaleID.CHIRP_FIRST_FISHING_CABIN_02,
            LocaleID.CHIRP_FIRST_FISHING_ISLAND,
            LocaleID.CHIRP_FIRST_FISH_FACTORY,
            LocaleID.CHIRP_FIRST_FISH_FARM_01,
            LocaleID.CHIRP_FIRST_FISH_FARM_02,
            LocaleID.CHIRP_FIRST_FISH_FARM_03,
            LocaleID.CHIRP_FIRST_FISH_MARKET,
            LocaleID.CHIRP_FIRST_FLAMINGO_ENCLOSURE,
            LocaleID.CHIRP_FIRST_FLOATING_CAFE,
            LocaleID.CHIRP_FIRST_FLOATING_GARBAGE_COLLECTOR,
            LocaleID.CHIRP_FIRST_FLOATING_GARDENS,
            LocaleID.CHIRP_FIRST_FLOUR_MILL,
            LocaleID.CHIRP_FIRST_FOOD_FACTORY,
            LocaleID.CHIRP_FIRST_FORESTRY_MAINTENANCE_BUILDING,
            LocaleID.CHIRP_FIRST_FORESTRY_MAIN_BUILDING_LEVEL_01,
            LocaleID.CHIRP_FIRST_FORESTRY_MAIN_BUILDING_LEVEL_02,
            LocaleID.CHIRP_FIRST_FORESTRY_MAIN_BUILDING_LEVEL_03,
            LocaleID.CHIRP_FIRST_FORESTRY_WORKERS_BARRACKS,
            LocaleID.CHIRP_FIRST_FRUIT_FIELD_01,
            LocaleID.CHIRP_FIRST_FRUIT_FIELD_02,
            LocaleID.CHIRP_FIRST_FRUIT_FIELD_03,
            LocaleID.CHIRP_FIRST_FRUIT_FIELD_GREENHOUSE_01,
            LocaleID.CHIRP_FIRST_FRUIT_FIELD_GREENHOUSE_02,
            LocaleID.CHIRP_FIRST_FRUIT_FIELD_GREENHOUSE_03,
            LocaleID.CHIRP_FIRST_FURNITURE_FACTORY,
            LocaleID.CHIRP_FIRST_GAME_BOOTH_01,
            LocaleID.CHIRP_FIRST_GAME_BOOTH_02,
            LocaleID.CHIRP_FIRST_GAZEBO_01,
            LocaleID.CHIRP_FIRST_GAZEBO_02,
            LocaleID.CHIRP_FIRST_GEOTHERMALPLANT,
            LocaleID.CHIRP_FIRST_GEOTHERMAL_POWER_PLANT,
            LocaleID.CHIRP_FIRST_GIRAFFE_ENCLOSURE,
            LocaleID.CHIRP_FIRST_GLASS_MANUFACTURING_PLANT,
            LocaleID.CHIRP_FIRST_GRAIN_SILO_01,
            LocaleID.CHIRP_FIRST_GRAIN_SILO_02,
            LocaleID.CHIRP_FIRST_HARBOR,
            LocaleID.CHIRP_FIRST_HIGH_SCHOOL,
            LocaleID.CHIRP_FIRST_HOT_AIR_BALLOON_TOURS,
            LocaleID.CHIRP_FIRST_HOUSEHOLD_PLASTIC_FACTORY,
            LocaleID.CHIRP_FIRST_HOUSE_OF_HORRORS,
            LocaleID.CHIRP_FIRST_HUNTING_CABIN_01,
            LocaleID.CHIRP_FIRST_HUNTING_CABIN_02,
            LocaleID.CHIRP_FIRST_INDUSTRIAL_STEEL_PLANT,
            LocaleID.CHIRP_FIRST_INLAND_WATER_TREATMENT_PLANT,
            LocaleID.CHIRP_FIRST_INSECT_AMPHIBIAN_REPTILE_HOUSE,
            LocaleID.CHIRP_FIRST_INTERCITY_BUS_STATION_01,
            LocaleID.CHIRP_FIRST_INTERCITY_BUS_STATION_02,
            LocaleID.CHIRP_FIRST_LANDFILL_SITE,
            LocaleID.CHIRP_FIRST_LARGEAIRCRAFTSTAND,
            LocaleID.CHIRP_FIRST_LARGEAIRPORT,
            LocaleID.CHIRP_FIRST_LARGETRAINSTATION,
            LocaleID.CHIRP_FIRST_LARGE_WAREHOUSE,
            LocaleID.CHIRP_FIRST_LARGE_WATER_TOWER,
            LocaleID.CHIRP_FIRST_LEANTO_SHELTER_01,
            LocaleID.CHIRP_FIRST_LEANTO_SHELTER_02,
            LocaleID.CHIRP_FIRST_LEMONADE_FACTORY,
            LocaleID.CHIRP_FIRST_LIBERAL_ARTS_ACADEMIC_STATUE_01,
            LocaleID.CHIRP_FIRST_LIBERAL_ARTS_ACADEMIC_STATUE_02,
            LocaleID.CHIRP_FIRST_LIBERAL_ARTS_ADMINISTRATION,
            LocaleID.CHIRP_FIRST_LIBERAL_ARTS_ART_CLUB,
            LocaleID.CHIRP_FIRST_LIBERAL_ARTS_AUDITORIUM,
            LocaleID.CHIRP_FIRST_LIBERAL_ARTS_BOOKSTORE,
            LocaleID.CHIRP_FIRST_LIBERAL_ARTS_CAFETERIA,
            LocaleID.CHIRP_FIRST_LIBERAL_ARTS_COMMENCEMENT_OFFICE,
            LocaleID.CHIRP_FIRST_LIBERAL_ARTS_DANCE_CLUB,
            LocaleID.CHIRP_FIRST_LIBERAL_ARTS_DORMITORY,
            LocaleID.CHIRP_FIRST_LIBERAL_ARTS_DRAMA_CLUB,
            LocaleID.CHIRP_FIRST_LIBERAL_ARTS_FOUNTAIN,
            LocaleID.CHIRP_FIRST_LIBERAL_ARTS_GROUNDSKEEPING,
            LocaleID.CHIRP_FIRST_LIBERAL_ARTS_GYMNASIUM,
            LocaleID.CHIRP_FIRST_LIBERAL_ARTS_LABORATORIES,
            LocaleID.CHIRP_FIRST_LIBERAL_ARTS_LIBRARY,
            LocaleID.CHIRP_FIRST_LIBERAL_ARTS_MEDIA_LAB,
            LocaleID.CHIRP_FIRST_LIBERAL_ARTS_MUSEUM,
            LocaleID.CHIRP_FIRST_LIBERAL_ARTS_OUTDOOR_STUDY,
            LocaleID.CHIRP_FIRST_LIBERAL_ARTS_STUDY_HALL,
            LocaleID.CHIRP_FIRST_LIBRARY,
            LocaleID.CHIRP_FIRST_LION_ENCLOSURE,
            LocaleID.CHIRP_FIRST_LOG_YARD_01,
            LocaleID.CHIRP_FIRST_LOG_YARD_02,
            LocaleID.CHIRP_FIRST_LOOKOUT_TOWER_01,
            LocaleID.CHIRP_FIRST_LOOKOUT_TOWER_02,
            LocaleID.CHIRP_FIRST_LUNGS_OF_THE_CITY,
            LocaleID.CHIRP_FIRST_MEDICALHELI,
            LocaleID.CHIRP_FIRST_MEDIUM_WAREHOUSE,
            LocaleID.CHIRP_FIRST_METEORITEPARK,
            LocaleID.CHIRP_FIRST_METRO_LINE,
            LocaleID.CHIRP_FIRST_MILKING_PARLOUR,
            LocaleID.CHIRP_FIRST_MODERN_TECHNOLOGY_INSTITUTE,
            LocaleID.CHIRP_FIRST_MODULAR_HOUSE_FACTORY,
            LocaleID.CHIRP_FIRST_MONKEY_PALACE,
            LocaleID.CHIRP_FIRST_MONORAILLINE,
            LocaleID.CHIRP_FIRST_MONUMENT,
            LocaleID.CHIRP_FIRST_MOOSE_AND_REINDEER_ENCLOSURE,
            LocaleID.CHIRP_FIRST_NAPHTHA_CRACKER_PLANT,
            LocaleID.CHIRP_FIRST_NATURE_RESERVE_MAIN_GATE,
            LocaleID.CHIRP_FIRST_NATURE_RESERVE_SIDE_GATE,
            LocaleID.CHIRP_FIRST_NUCLEAR_PLANT,
            LocaleID.CHIRP_FIRST_OBSERVATION_TOWER,
            LocaleID.CHIRP_FIRST_OCEAN_THERMAL_ENERGY_CONVERSION_PLANT,
            LocaleID.CHIRP_FIRST_OFFSHORE_OIL_DRILLING_PLATFORM,
            LocaleID.CHIRP_FIRST_OIL_DRILLING_RIG_01,
            LocaleID.CHIRP_FIRST_OIL_DRILLING_RIG_02,
            LocaleID.CHIRP_FIRST_OIL_INDUSTRY_STORAGE,
            LocaleID.CHIRP_FIRST_OIL_MAINTENANCE_BUILDING,
            LocaleID.CHIRP_FIRST_OIL_MAIN_BUILDING_LEVEL_01,
            LocaleID.CHIRP_FIRST_OIL_MAIN_BUILDING_LEVEL_02,
            LocaleID.CHIRP_FIRST_OIL_MAIN_BUILDING_LEVEL_03,
            LocaleID.CHIRP_FIRST_OIL_PUMP_01,
            LocaleID.CHIRP_FIRST_OIL_PUMP_02,
            LocaleID.CHIRP_FIRST_OIL_SLUDGE_PYROLYSIS_PLANT,
            LocaleID.CHIRP_FIRST_OIL_WORKERS_BARRACKS,
            LocaleID.CHIRP_FIRST_OLD_MARKET_STREET,
            LocaleID.CHIRP_FIRST_ORE_GRINDING_MILL,
            LocaleID.CHIRP_FIRST_ORE_INDUSTRY_STORAGE,
            LocaleID.CHIRP_FIRST_ORE_MAINTENANCE_BUILDING,
            LocaleID.CHIRP_FIRST_ORE_MAIN_BUILDING_LEVEL_01,
            LocaleID.CHIRP_FIRST_ORE_MAIN_BUILDING_LEVEL_02,
            LocaleID.CHIRP_FIRST_ORE_MAIN_BUILDING_LEVEL_03,
            LocaleID.CHIRP_FIRST_ORE_MINE_01,
            LocaleID.CHIRP_FIRST_ORE_MINE_02,
            LocaleID.CHIRP_FIRST_ORE_MINE_03,
            LocaleID.CHIRP_FIRST_ORE_MINE_UNDERGROUND_01,
            LocaleID.CHIRP_FIRST_ORE_MINE_UNDERGROUND_02,
            LocaleID.CHIRP_FIRST_ORE_STORAGE,
            LocaleID.CHIRP_FIRST_ORE_WORKERS_BARRACKS,
            LocaleID.CHIRP_FIRST_OVERGROUND_METRO_STATION,
            LocaleID.CHIRP_FIRST_OVERGROUND_METRO_STATION_ELEVATED,
            LocaleID.CHIRP_FIRST_PARK_CHESS_BOARD_01,
            LocaleID.CHIRP_FIRST_PARK_INFO_BOOTH_01,
            LocaleID.CHIRP_FIRST_PARK_MAINTENANCE_BUILDING,
            LocaleID.CHIRP_FIRST_PARK_MAIN_GATE,
            LocaleID.CHIRP_FIRST_PARK_PIER_01,
            LocaleID.CHIRP_FIRST_PARK_PIER_02,
            LocaleID.CHIRP_FIRST_PARK_PLAZA,
            LocaleID.CHIRP_FIRST_PARK_RESTROOMS_01,
            LocaleID.CHIRP_FIRST_PARK_SIDE_GATE,
            LocaleID.CHIRP_FIRST_PASSENGER_HELICOPTER_DEPOT,
            LocaleID.CHIRP_FIRST_PASSENGER_HELICOPTER_STOP,
            LocaleID.CHIRP_FIRST_PENDULUM_RIDE,
            LocaleID.CHIRP_FIRST_PETROCHEMICAL_PLANT,
            LocaleID.CHIRP_FIRST_PETROLEUM_REFINERY,
            LocaleID.CHIRP_FIRST_PIGGY_TRAIN,
            LocaleID.CHIRP_FIRST_POLICEHELI,
            LocaleID.CHIRP_FIRST_POLICE_ACADEMY,
            LocaleID.CHIRP_FIRST_POLICE_HQ,
            LocaleID.CHIRP_FIRST_POLICE_STATION,
            LocaleID.CHIRP_FIRST_POST_OFFICE,
            LocaleID.CHIRP_FIRST_POST_SORTING_FACILITY,
            LocaleID.CHIRP_FIRST_POWER_PLANT,
            LocaleID.CHIRP_FIRST_PRINTING_PRESS,
            LocaleID.CHIRP_FIRST_PRISON,
            LocaleID.CHIRP_FIRST_PULP_MILL,
            LocaleID.CHIRP_FIRST_PUMPING,
            LocaleID.CHIRP_FIRST_RAW_MINERAL_STORAGE,
            LocaleID.CHIRP_FIRST_RECYCLING_CENTER,
            LocaleID.CHIRP_FIRST_RHINO_ENCLOSURE,
            LocaleID.CHIRP_FIRST_ROADMAINTENANCEDEPOT,
            LocaleID.CHIRP_FIRST_ROLLERCOASTER,
            LocaleID.CHIRP_FIRST_ROTARY_KILN_PLANT,
            LocaleID.CHIRP_FIRST_ROTATING_TEA_CUPS,
            LocaleID.CHIRP_FIRST_SAND_STORAGE,
            LocaleID.CHIRP_FIRST_SAUNA,
            LocaleID.CHIRP_FIRST_SAW_DUST_STORAGE,
            LocaleID.CHIRP_FIRST_SAW_MILL,
            LocaleID.CHIRP_FIRST_SCHOOL_OF_ECONOMISC,
            LocaleID.CHIRP_FIRST_SCHOOL_OF_EDUCATION,
            LocaleID.CHIRP_FIRST_SCHOOL_OF_ENGINEERING,
            LocaleID.CHIRP_FIRST_SCHOOL_OF_ENVIRONMENTAL_STUDIES,
            LocaleID.CHIRP_FIRST_SCHOOL_OF_LAW,
            LocaleID.CHIRP_FIRST_SCHOOL_OF_MEDICINE,
            LocaleID.CHIRP_FIRST_SCHOOL_OF_SCIENCE,
            LocaleID.CHIRP_FIRST_SCHOOL_OF_TOURISM_AND_TRAVEL,
            LocaleID.CHIRP_FIRST_SEABED_MINING_VESSEL,
            LocaleID.CHIRP_FIRST_SEALIFE_ENCLOSURE,
            LocaleID.CHIRP_FIRST_SEA_FORTRESS,
            LocaleID.CHIRP_FIRST_SHELTER,
            LocaleID.CHIRP_FIRST_SIGHTSEEING_BUS_DEPOT,
            LocaleID.CHIRP_FIRST_SKIRESORT,
            LocaleID.CHIRP_FIRST_SLAUGHTER_HOUSE,
            LocaleID.CHIRP_FIRST_SMALL_WAREHOUSE,
            LocaleID.CHIRP_FIRST_SNEAKER_FACTORY,
            LocaleID.CHIRP_FIRST_SNOWDUMP,
            LocaleID.CHIRP_FIRST_SOFT_PAPER_FACTORY,
            LocaleID.CHIRP_FIRST_SOLAR_UPDRAFT_TOWER,
            LocaleID.CHIRP_FIRST_SPAHOTEL,
            LocaleID.CHIRP_FIRST_SPORTS_HALL_AND_GYMNASIUM,
            LocaleID.CHIRP_FIRST_STABLE,
            LocaleID.CHIRP_FIRST_STEAMTRAIN,
            LocaleID.CHIRP_FIRST_SWINGING_BOAT,
            LocaleID.CHIRP_FIRST_TAXIDEPOT,
            LocaleID.CHIRP_FIRST_TENT_01,
            LocaleID.CHIRP_FIRST_TENT_02,
            LocaleID.CHIRP_FIRST_TENT_03,
            LocaleID.CHIRP_FIRST_TENT_CAMPING_SITE_01_,
            LocaleID.CHIRP_FIRST_THE_STATUE_OF_COLOSSALUS,
            LocaleID.CHIRP_FIRST_TOWN_HALL,
            LocaleID.CHIRP_FIRST_TOY_FACTORY,
            LocaleID.CHIRP_FIRST_TRACK_AND_FIELD_STADIUM,
            LocaleID.CHIRP_FIRST_TRADE_SCHOOL_ACADEMIC_STATUE_01,
            LocaleID.CHIRP_FIRST_TRADE_SCHOOL_ACADEMIC_STATUE_02,
            LocaleID.CHIRP_FIRST_TRADE_SCHOOL_ADMINISTRATION,
            LocaleID.CHIRP_FIRST_TRADE_SCHOOL_AUDITORIUM,
            LocaleID.CHIRP_FIRST_TRADE_SCHOOL_BEACH_VOLLEYBALL_CLUB,
            LocaleID.CHIRP_FIRST_TRADE_SCHOOL_BOOKSTORE,
            LocaleID.CHIRP_FIRST_TRADE_SCHOOL_BOOK_CLUB,
            LocaleID.CHIRP_FIRST_TRADE_SCHOOL_CAFETERIA,
            LocaleID.CHIRP_FIRST_TRADE_SCHOOL_COMMENCEMENT_OFFICE,
            LocaleID.CHIRP_FIRST_TRADE_SCHOOL_DORMITORY,
            LocaleID.CHIRP_FIRST_TRADE_SCHOOL_FOUNTAIN_01,
            LocaleID.CHIRP_FIRST_TRADE_SCHOOL_GROUNDSKEEPING,
            LocaleID.CHIRP_FIRST_TRADE_SCHOOL_GYMNASIUM,
            LocaleID.CHIRP_FIRST_TRADE_SCHOOL_IT_CLUB,
            LocaleID.CHIRP_FIRST_TRADE_SCHOOL_LABORATORIES,
            LocaleID.CHIRP_FIRST_TRADE_SCHOOL_LIBRARY,
            LocaleID.CHIRP_FIRST_TRADE_SCHOOL_MEDIA_LAB,
            LocaleID.CHIRP_FIRST_TRADE_SCHOOL_MUSEUM,
            LocaleID.CHIRP_FIRST_TRADE_SCHOOL_OUTDOOR_STUDY,
            LocaleID.CHIRP_FIRST_TRADE_SCHOOL_STUDY_HALL,
            LocaleID.CHIRP_FIRST_TRAFFICPARK,
            LocaleID.CHIRP_FIRST_TRAIN_LINE,
            LocaleID.CHIRP_FIRST_TRAMDEPOT,
            LocaleID.CHIRP_FIRST_TRAMPOLINE_PARK_01,
            LocaleID.CHIRP_FIRST_TRANSPORT_HUB_01,
            LocaleID.CHIRP_FIRST_TRANSPORT_HUB_02,
            LocaleID.CHIRP_FIRST_TRANSPORT_HUB_03,
            LocaleID.CHIRP_FIRST_TRANSPORT_HUB_04,
            LocaleID.CHIRP_FIRST_TRANSPORT_HUB_05,
            LocaleID.CHIRP_FIRST_TREE_PLANTATION_01,
            LocaleID.CHIRP_FIRST_TREE_PLANTATION_02,
            LocaleID.CHIRP_FIRST_TREE_PLANTATION_03,
            LocaleID.CHIRP_FIRST_TREE_SAPLING_01,
            LocaleID.CHIRP_FIRST_TREE_SAPLING_02,
            LocaleID.CHIRP_FIRST_TREE_SAPLING_GREENHOUSE_01,
            LocaleID.CHIRP_FIRST_TREE_SAPLING_GREENHOUSE_02,
            LocaleID.CHIRP_FIRST_TROLLEYBUS_DEPOT,
            LocaleID.CHIRP_FIRST_TROLLEYBUS_STOP,
            LocaleID.CHIRP_FIRST_TROPICAL_GARDEN,
            LocaleID.CHIRP_FIRST_TSUNAMIBOYO,
            LocaleID.CHIRP_FIRST_UB1,
            LocaleID.CHIRP_FIRST_UB2,
            LocaleID.CHIRP_FIRST_UB3,
            LocaleID.CHIRP_FIRST_ULTIMATE_RECYCLING_PLANT,
            LocaleID.CHIRP_FIRST_UNIVERSITY,
            LocaleID.CHIRP_FIRST_UNIVERSITY_ACADEMIC_STATUE_01,
            LocaleID.CHIRP_FIRST_UNIVERSITY_ACADEMIC_STATUE_02,
            LocaleID.CHIRP_FIRST_UNIVERSITY_ADMINISTRATION,
            LocaleID.CHIRP_FIRST_UNIVERSITY_AUDITORIUM,
            LocaleID.CHIRP_FIRST_UNIVERSITY_BOOKSTORE,
            LocaleID.CHIRP_FIRST_UNIVERSITY_CAFETERIA,
            LocaleID.CHIRP_FIRST_UNIVERSITY_CHESS_CLUB,
            LocaleID.CHIRP_FIRST_UNIVERSITY_COMMENCEMENT_OFFICE,
            LocaleID.CHIRP_FIRST_UNIVERSITY_DORMITORY,
            LocaleID.CHIRP_FIRST_UNIVERSITY_FOUNTAIN,
            LocaleID.CHIRP_FIRST_UNIVERSITY_GROUDSKEEPING,
            LocaleID.CHIRP_FIRST_UNIVERSITY_GYMNASIUM,
            LocaleID.CHIRP_FIRST_UNIVERSITY_LABORATORIES,
            LocaleID.CHIRP_FIRST_UNIVERSITY_LIBRARY,
            LocaleID.CHIRP_FIRST_UNIVERSITY_MATH_CLUB,
            LocaleID.CHIRP_FIRST_UNIVERSITY_MEDIA_LAB,
            LocaleID.CHIRP_FIRST_UNIVERSITY_MUSEUM,
            LocaleID.CHIRP_FIRST_UNIVERSITY_OF_CREATIVE_ARTS,
            LocaleID.CHIRP_FIRST_UNIVERSITY_OUTDOOR_STUDY,
            LocaleID.CHIRP_FIRST_UNIVERSITY_SOCCER_CLUB,
            LocaleID.CHIRP_FIRST_UNIVERSITY_STUDY_HALL,
            LocaleID.CHIRP_FIRST_VIEWING_DECK_01,
            LocaleID.CHIRP_FIRST_VIEWING_DECK_02,
            LocaleID.CHIRP_FIRST_WAREHOUSE_YARD,
            LocaleID.CHIRP_FIRST_WASTEPROCESSING_COMPLEX,
            LocaleID.CHIRP_FIRST_WASTE_OIL_REFINING_PLANT,
            LocaleID.CHIRP_FIRST_WASTE_TRANSFER_FACILITY,
            LocaleID.CHIRP_FIRST_WATERTANK,
            LocaleID.CHIRP_FIRST_WATER_PUMP,
            LocaleID.CHIRP_FIRST_WEATHERSTATION,
            LocaleID.CHIRP_FIRST_WONDER,
            LocaleID.CHIRP_FIRST_WOOD_CHIP_STORAGE,
            LocaleID.CHIRP_FIRST_YOGA_GARDEN,
            LocaleID.CHIRP_FIRST_YOGA_GARDEN_WINTER,
            LocaleID.CHIRP_FIRST_ZIGGURAT_GARDEN,
            LocaleID.CHIRP_FIRST_ZOO_MAIN_GATE,
            LocaleID.CHIRP_FIRST_ZOO_RESTROOMS_01,
            LocaleID.CHIRP_FIRST_ZOO_SIDE_GATE,
            LocaleID.CHIRP_FIRST_ZOO_SOUVENIR_SHOP_01,
            LocaleID.FOOTBALLCHIRP_FIRST_FSTADIUM,
            LocaleID.CHIRP_AIRPORTBUILDING,
            LocaleID.CHIRP_FIRST_HIGHDENSITYHIGHSCHOOL,
            LocaleID.CHIRP_FIRST_HIGHDENSITYELEMENTARYSCHOOL,
            LocaleID.CHIRP_FIRST_HIGHDENSITYUNIVERSITY,
            LocaleID.CHIRP_FIRST_HIGHDENSITYHOSPITAL,
            LocaleID.CHIRP_FIRST_HIGHDENSITYPOLICESTATION,
            LocaleID.CHIRP_FIRST_HIGHDENSITYFIRESTATION,
            LocaleID.CHIRP_FIRST_ELEVATEDTRAINSTATION,
            LocaleID.CHIRP_FIRST_ELEVATEDMETROSTATION,
            LocaleID.CHIRP_FIRST_HIGHDENSITYMETRO,
            LocaleID.CHIRP_FIRST_HIGHDENSITYMETROSTATION,
            LocaleID.CHIRP_FIRST_HIGHDENSITYBUSSTATION,
            LocaleID.CHIRP_FIRST_PEDESTRIANSERVICEPOINT,
            LocaleID.CHIRP_FIRST_PEDESTRIANSERVICEPOINT2,
            LocaleID.CHIRP_FIRST_GARBAGESERVICEPOINT,
            LocaleID.CHIRP_FIRST_GARBAGESERVICEPOINT2,
            LocaleID.CHIRP_FIRST_CARGOSERVICEPOINT,
            LocaleID.CHIRP_FIRST_CARGOSERVICEPOINT2,
            LocaleID.CHIRP_FIRST_PEDESTRIANAREAPLAZASUMMER,
            LocaleID.CHIRP_FIRST_PEDESTRIANAREAPLAZAWINTER,
            LocaleID.CHIRP_FIRST_SMALLPEDESTRIANAREAPLAZA,
            LocaleID.CHIRP_FIRST_ICECREAMSTAND,
            LocaleID.CHIRP_FIRST_ICECREAMSTAND2,
            LocaleID.CHIRP_FIRST_FOODTRUCK,
            LocaleID.CHIRP_FIRST_FOODTRUCK2,
            LocaleID.CHIRP_FIRST_STATUEPLAZA,
            LocaleID.CHIRP_FIRST_FLOWERPLAZA,
            LocaleID.CHIRP_FIRST_SMALLFOUNTAIN,
            LocaleID.CHIRP_FIRST_LARGEFOUNTAIN,
            LocaleID.CHIRP_FIRST_LANDMARKOFFICEHIGH,
            LocaleID.CHIRP_FIRST_LANDMARKRESIDENTIALHIGH,
            LocaleID.CHIRP_FIRST_LANDMARKCOMMERCIALHIGH,
            LocaleID.CHIRP_FIRST_LANDMARKMARKETHALL,
            LocaleID.CHIRP_FIRST_LANDMARKMUSEUM,
            LocaleID.CHIRP_FIRST_LANDMARKSHOPPINGMALL,
        };

        public static readonly HashSet<string> ServiceBuilt = new HashSet<string>
        {
            LocaleID.CHIRP_NEW_FESTIVALAREA,
            LocaleID.CHIRP_NEW_FIRE_STATION,
            LocaleID.CHIRP_NEW_HOSPITAL,
            LocaleID.CHIRP_NEW_MAP_TILE,
            LocaleID.CHIRP_NEW_MONUMENT,
            LocaleID.CHIRP_NEW_PARK,
            LocaleID.CHIRP_NEW_PLAZA,
            LocaleID.CHIRP_NEW_POLICE_HQ,
            LocaleID.CHIRP_NEW_TILE_PLACED,
            LocaleID.CHIRP_NEW_TOLL_BOOTH,
            LocaleID.CHIRP_NEW_UNIVERSITY,
            LocaleID.CHIRP_NEW_WIND_OR_SOLAR_PLANT,
        };

        public static readonly HashSet<string> ChirpX = new HashSet<string>
        {
            LocaleID.CHIRP_LAUNCH, LocaleID.CHIRP_LAUNCH_PREPARATION, LocaleID.CHIRP_ROCKET_PRODUCTION,
        };

        public static readonly HashSet<string> Football = new HashSet<string>
        {
            LocaleID.FOOTBALLCHIRP_GENERIC, LocaleID.FOOTBALLCHIRP_LOSE, LocaleID.FOOTBALLCHIRP_WIN,
        };

        public static readonly HashSet<string> Random = new HashSet<string>
        {
            LocaleID.CHIRP_RANDOM,
            LocaleID.CHIRP_RANDOM_DISASTERS,
            LocaleID.CHIRP_RANDOM_EXP10,
            LocaleID.CHIRP_RANDOM_EXP11,
            LocaleID.CHIRP_RANDOM_EXP5,
            LocaleID.CHIRP_RANDOM_EXP6,
            LocaleID.CHIRP_RANDOM_EXP7,
            LocaleID.CHIRP_RANDOM_EXP8,
            LocaleID.CHIRP_RANDOM_EXP9,
            LocaleID.CHIRP_RANDOM_INMOTION,
            LocaleID.CHIRP_RANDOM_BIRTHDAY,
            LocaleID.CHIRP_CHEAP_FLOWERS,
        };

        public static readonly HashSet<string> Concerts = new HashSet<string>
        {
            LocaleID.CHIRP_BAND_LILY,
            LocaleID.CHIRP_BAND_MOTI,
            LocaleID.CHIRP_BAND_NESTOR,
            LocaleID.CHIRP_UPGRADE_FESTIVALAREA,
        };

        public static readonly HashSet<string> Celebrations = new HashSet<string>
        {
            LocaleID.CHIRP_MILESTONE_REACHED,
            LocaleID.CHIRP_LOW_CRIME,
            LocaleID.CHIRP_HAPPY_PEOPLE,
            LocaleID.CHIRP_ATTRACTIVE_CITY,
            LocaleID.CHIRP_PUBLIC_TRANSPORT_EFFICIENCY,
            LocaleID.CHIRP_SURVIVORFOUND,
        };

        public static readonly HashSet<string> Problems = new HashSet<string>
        {
            LocaleID.CHIRP_ABANDONED_BUILDINGS,
            LocaleID.CHIRP_DEAD_PILING_UP,
            LocaleID.CHIRP_HIGH_CRIME,
            LocaleID.CHIRP_LOW_HAPPINESS,
            LocaleID.CHIRP_LOW_HEALTH,
            LocaleID.CHIRP_NEED_MORE_PARKS,
            LocaleID.CHIRP_NOISEPOLLUTION,
            LocaleID.CHIRP_NO_ELECTRICITY,
            LocaleID.CHIRP_NO_HEALTHCARE,
            LocaleID.CHIRP_NO_PRISONS,
            LocaleID.CHIRP_NO_SCHOOLS,
            LocaleID.CHIRP_NO_WATER,
            LocaleID.CHIRP_POISONED,
            LocaleID.CHIRP_POLLUTION,
            LocaleID.CHIRP_SEWAGE,
            LocaleID.CHIRP_TRASH_PILING_UP,
            LocaleID.CHIRP_RESIDENTIAL_DEMAND,
            LocaleID.CHIRP_COMMERCIAL_DEMAND,
            LocaleID.CHIRP_INDUSTRIAL_DEMAND,
            LocaleID.CHIRP_FIRE_HAZARD,
        };


        public static readonly HashSet<string> VarsitySports = new HashSet<string>
        {
            LocaleID.VARSITYSPORTSCHIRP_GENERIC, LocaleID.VARSITYSPORTSCHIRP_LOSE, LocaleID.VARSITYSPORTSCHIRP_WIN,
        };

        public static readonly HashSet<string> UnlockedFishingBuildings = new HashSet<string>
        {
            LocaleID.CHIRP_FISHING_BOAT_HARBOR_02_UNLOCKED,
            LocaleID.CHIRP_FISHING_BOAT_HARBOR_03_UNLOCKED,
            LocaleID.CHIRP_FISHING_BOAT_HARBOR_04_UNLOCKED,
            LocaleID.CHIRP_FISHING_BOAT_HARBOR_05_UNLOCKED,
            LocaleID.CHIRP_FISH_FARM_02_UNLOCKED,
            LocaleID.CHIRP_FISH_FARM_03_UNLOCKED,
        };

        public static readonly HashSet<string> TogaPartiesAndGraduations = new HashSet<string>
        {
            LocaleID.GRADUATIONCHIRP_GENERIC,
            LocaleID.TOGAPARTYCHIRP_GENERIC,
        };

        public static readonly HashSet<string> PoliciesAndThemes = new HashSet<string>
        {
            LocaleID.CHIRP_POLICY,
            LocaleID.CHIRP_HIGH_TECH_LEVEL,
            LocaleID.CHIRP_ORGANIC_FARMING,
            LocaleID.CHIRP_RANDOM_THEME,
        };

        public static readonly HashSet<string> Disasters = new HashSet<string>
        {
            LocaleID.CHIRP_DISASTER,
        };

        /// <summary>
        /// Chirps I haven't figured out how to spawn
        /// </summary>
        public static readonly HashSet<string> Uncategorized = new HashSet<string>
        {
            LocaleID.CHIRP_ASSISTIVE_TECHNOLOGIES,
            LocaleID.CHIRP_DAYCARE_SERVICE,
            LocaleID.CHIRP_STUDENT_LODGING,
        };
    }
}
