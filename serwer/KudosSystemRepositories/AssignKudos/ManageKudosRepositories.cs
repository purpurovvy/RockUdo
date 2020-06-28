using AssignKudosContext.Entities;
using AssignKudosContext.Repositories;
using DatabaseContext;
using DatabaseContext.Models;
using System;
using System.Linq;
using KudosSystemRepositories.QueryOrder;
using AssignKudosContext.Repositories.Parameters;
using System.Collections.Generic;

namespace KudosSystemRepositories.AssignKudos
{
    public class ManageKudosRepositories : ISaveKudosRepository, IGetKudosRepository
    {
        EkudosContext _context;
        public ManageKudosRepositories(EkudosContext context)
        {
            _context = context;
        }
        
        public bool SaveKudos(Kudos kudos)
        {
            try
            {
                _context.Add(new EkudosModel()
                {
                    Id = Guid.NewGuid(),
                    Who = kudos.Recipient.Name,
                    Donator = kudos.Donator.Name,
                    ForWhat = kudos.ForWhat,
                    When = kudos.When,
                    Giphy = kudos.Giphy
                });
                _context.SaveChanges();

                return true;
            }
            catch
            {
                // TODO to add logging errors
                return false;
            }
        }

        public IQueryable<Kudos> SimpleList(IFilter filter)
        {

            IQueryable<EkudosModel> query = _context
                .Kudoses;

            if(filter.FilterList.Count() > 0)
            {
                if(filter.FilterList.Any(x => x.name == "whoms"))
                {
                    string value = filter
                        .FilterList
                        .FirstOrDefault(x => x.name == "whoms")
                        .value;

                    if (!string.IsNullOrEmpty(value))
                    {
                        List<string> listElemenets = value
                       .Split(",")
                       .ToList();

                        if (listElemenets.Count() > 0)
                        {
                            query = query.Where(x => listElemenets.Contains(x.Who));
                        }
                    }
                   
                }

                if (filter.FilterList.Any(x => x.name == "donators"))
                {
                    string value = filter
                        .FilterList
                        .FirstOrDefault(x => x.name == "donators")
                        .value;

                    if (!string.IsNullOrEmpty(value))
                    {
                        List<string> listElemenets = value
                       .Split(",")
                       .ToList();

                        if (listElemenets.Count() > 0)
                        {
                            query = query.Where(x => listElemenets.Contains(x.Donator));
                        }
                    }

                }
            }

            query = query.AsQueryable()
                .OrderByDynamic(filter.Sort ?? "When", (QueryOrder.Order)(int)filter.Order);

            if (filter.From >= 0)
            {
                query = query.Skip(filter.From);
            }

            if (filter.HowMany > 0)
            {
                query = query.Take(filter.HowMany);
            }
            
            return query.Select(x => new Kudos
            {
                Id = x.Id,
                Donator = new Donator { Name = x.Donator },
                ForWhat = x.ForWhat,
                Recipient = new Recipient { Name = x.Who },
                When = x.When,
                Giphy = x.Giphy
            });
        }
    }
}
