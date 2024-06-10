using AutoMapper;
using BloodGL.Application.Dtos;
using BloodGL.Core.Database.Firebase;
using BloodGL.Domain.Interfaces;
using BloodGL.Domain.Models;
using Microsoft.Extensions.Configuration;

namespace BloodGL.Application.Services
{
	public class GlucoseMeasureReplyService : IGlucoseMeasureReplyService
	{
		private readonly IGlucoseMeasureReplyRepository glucoseMeasureReplyRepository;
		private readonly IGlucoseMeasureRepository glucoseMeasureRepository;
		private readonly IUserDeviceRepository userDeviceRepository;
		private readonly IMapper mapper;
		private readonly IConfiguration configuration;
		private readonly FirebaseNotification firebaseNotification;

		public GlucoseMeasureReplyService(IGlucoseMeasureReplyRepository glucoseMeasureReplyRepository, IGlucoseMeasureRepository glucoseMeasureRepository, IUserDeviceRepository userDeviceRepository, IMapper mapper, IConfiguration configuration, FirebaseNotification firebaseNotification)
		{
			this.glucoseMeasureReplyRepository = glucoseMeasureReplyRepository;
			this.glucoseMeasureRepository = glucoseMeasureRepository;
			this.userDeviceRepository = userDeviceRepository;
			this.mapper = mapper;
			this.configuration = configuration;
			this.firebaseNotification = firebaseNotification;
		}

		public async Task Add(string measureId, string reply)
		{
			var measure = await glucoseMeasureRepository.GetById(measureId);

			if (measure == null) { return; }

			await glucoseMeasureReplyRepository.Add(new GlucoseMeasureReplyEntity
			{
				GlucoseMeasureId = measureId,
				Reply = reply
			});

			var userDevices =await userDeviceRepository.GetMany(x=>x.UserId== measure.UserId);

			foreach (var device in userDevices)
			{
				await firebaseNotification.SendNotification("Doctor Reply", reply, device.Token);
			}
		}

		public async Task<IEnumerable<GlucoseMeasureReplyDto>> GetGlucoseMeasureReplies(string measureId)
		{
			var replyEntities= await glucoseMeasureReplyRepository.GetMany(x=>x.GlucoseMeasureId==measureId);
			var replies=mapper.Map<IEnumerable<GlucoseMeasureReplyDto>>(replyEntities);

			return replies;
		}
	}
}
