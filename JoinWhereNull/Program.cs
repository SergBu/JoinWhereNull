var yearStarted = DateTime.UtcNow.AddYears(-1);

var iDsWhereStateExists = await GetIdsAsync(context);

var terminalTimeslotVehicles = await (from v in context.TerminalTimeslotVehicles.AsNoTracking()
                                      where v.DateCreated > yearStarted
                                      join s in context.TerminalTimeslotVehicleStates.AsNoTracking()
                                           on v.Id equals s.TerminalTimeslotVehicleId into vs
                                      from s in vs.DefaultIfEmpty()
                                      where s == null
                                      orderby v.Id
                                      select v
                                )
                                .Take(10000)
                                .ToListAsync();