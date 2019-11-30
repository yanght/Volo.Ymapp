using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Volo.Ymapp.Kh10086
{
    public class LineAppService : ApplicationService, ILineAppService
    {
        private readonly IRepository<Line, long> _lineRepository;
        private readonly IRepository<LineDay, long> _lineDayRepository;
        private readonly IRepository<LineDayImage, long> _lineDayImageRepository;
        private readonly IRepository<LineDaySelf, long> _lineDaySelfRepository;
        private readonly IRepository<LineDayShop, long> _lineDayShopRepository;
        private readonly IRepository<LineDayTraffic, long> _lineDayTrafficRepository;
        private readonly IRepository<LineIntro, long> _lineIntroRepository;
        private readonly IRepository<LineRouteDate, long> _lineRouteDateRepository;
        private readonly IRepository<LineTeam, long> _lineTeamRepository;
        public LineAppService(IRepository<Line, long> lineRepository
            , IRepository<LineDay, long> lineDayRepository
            , IRepository<LineDayImage, long> lineDayImageRepository
            , IRepository<LineDaySelf, long> lineDaySelfRepository
            , IRepository<LineDayShop, long> lineDayShopRepository
            , IRepository<LineDayTraffic, long> lineDayTrafficRepository
            , IRepository<LineIntro, long> lineIntroRepository
            , IRepository<LineRouteDate, long> lineRouteDateRepository
            , IRepository<LineTeam, long> lineTeamRepository)
        {
            _lineRepository = lineRepository;
            _lineDayRepository = lineDayRepository;
            _lineDayImageRepository = lineDayImageRepository;
            _lineDaySelfRepository = lineDaySelfRepository;
            _lineDayShopRepository = lineDayShopRepository;
            _lineDayTrafficRepository = lineDayTrafficRepository;
            _lineIntroRepository = lineIntroRepository;
            _lineRouteDateRepository = lineRouteDateRepository;
            _lineTeamRepository = lineTeamRepository;

        }

        public async Task InsertLine(LineDto dto)
        {
            var line = await _lineRepository.InsertAsync(new Line()
            {
                Continent = dto.Continent,
                Country = dto.Country,
                CustomTitle = dto.CustomTitle,
                FirstLineImg = dto.FirstLineImg,
                Function = dto.Function,
                ImgCity = dto.ImgCity,
                ImgCode = dto.ImgCode,
                ImgContinent = dto.ImgContinent,
                ImgCountry = dto.ImgCountry,
                LineCode = dto.LineCode,
                LineType = dto.LineType,
                NumDay = dto.NumDay,
                NumNight = dto.NumNight,
                PlaceLeave = dto.PlaceLeave,
                PlaceReturn = dto.PlaceReturn,
                Sight = dto.Sight,
                Title = dto.Title,
                TxtTransitCity = dto.TxtTransitCity,
                Visa = dto.Visa,
            });

            if (dto.LineIntros != null & dto.LineIntros.Count > 0)
            {
                dto.LineIntros.ForEach(async (m) =>
                {
                    var lineIntro = ObjectMapper.Map<LineIntroDto, LineIntro>(m);
                    lineIntro.LineId = line.Id;
                    await _lineIntroRepository.InsertAsync(lineIntro);
                });
            }

            if (dto.LineTeams != null && dto.LineTeams.Count > 0)
            {
                dto.LineTeams.ForEach(async (m) =>
                {
                    var lineTeam = ObjectMapper.Map<LineTeamDto, LineTeam>(m);
                    lineTeam.LineId = line.Id;
                    await _lineTeamRepository.InsertAsync(lineTeam);
                });
            }

            if (dto.LineDays != null && dto.LineDays.Count > 0)
            {
                dto.LineDays.ForEach(async (m) =>
                {
                    var lineDay = await _lineDayRepository.InsertAsync(new LineDay()
                    {
                        Breakfast = m.Breakfast,
                        CityEnglish = m.CityEnglish,
                        DayHotel = m.DayHotel,
                        DayNumber = m.DayNumber,
                        DayTraffic = m.DayTraffic,
                        Describe = m.Describe,
                        Dinner = m.Dinner,
                        LineId = line.Id,
                        Lunch = m.Lunch,
                        ScityDistance = m.ScityDistance,
                        TrafficName = m.TrafficName,
                    });

                    if (m.LineDayImages != null && m.LineDayImages.Count > 0)
                    {
                        m.LineDayImages.ForEach(async (img) =>
                        {
                            var image = ObjectMapper.Map<LineDayImageDto, LineDayImage>(img);
                            image.LineDayId = lineDay.Id;
                            img.LineId = line.Id;
                            await _lineDayImageRepository.InsertAsync(image);
                        });
                    }

                    if (m.LineDaySelfs != null && m.LineDaySelfs.Count > 0)
                    {
                        m.LineDaySelfs.ForEach(async (self) =>
                        {
                            var daySelf = ObjectMapper.Map<LineDaySelfDto, LineDaySelf>(self);
                            daySelf.LineId = line.Id;
                            daySelf.LineDayId = lineDay.Id;
                            await _lineDaySelfRepository.InsertAsync(daySelf);
                        });
                    }

                    if (m.LineDayShops != null && m.LineDayShops.Count > 0)
                    {
                        m.LineDayShops.ForEach(async (shop) =>
                        {
                            var dayShop = ObjectMapper.Map<LineDayShopDto, LineDayShop>(shop);
                            dayShop.LineId = line.Id;
                            dayShop.LineDayId = lineDay.Id;
                            await _lineDayShopRepository.InsertAsync(dayShop);
                        });
                    }

                    if (m.LineDayTraffics != null && m.LineDayTraffics.Count > 0)
                    {
                        m.LineDayTraffics.ForEach(async (traffic) =>
                        {
                            var dayTraffic = ObjectMapper.Map<LineDayTrafficDto, LineDayTraffic>(traffic);
                            dayTraffic.LineId = line.Id;
                            dayTraffic.LineDayId = lineDay.Id;
                            await _lineDayTrafficRepository.InsertAsync(dayTraffic);
                        });
                    }

                });
            }

            _ = Task.FromResult(true);
        }
    }
}
