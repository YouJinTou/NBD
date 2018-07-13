﻿using AutoMapper;
using NBD.SDK;
using NBD.Tracker.Models;

namespace NBD.Tracker.Configuration
{
    public class MapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Goal, GoalViewModel>();
                cfg.CreateMap<GoalBindingModel, Goal>();
                cfg.CreateMap<GoalTreeBindingModel, GoalTree>()
                    .ForMember(d => d.RootId, o => o.MapFrom(s => s.RootGoal.RootId));
            });
        }
    }
}
