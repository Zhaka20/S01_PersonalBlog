using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using S01_PersonalBlog.Models;
using S01_PersonalBlog.ViewModels;
using AutoMapper.QueryableExtensions;
using S01_PersonalBlog.DTOs;
using Microsoft.AspNet.Identity;


namespace S01_PersonalBlog
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ApplicationUser, AuthorViewModel>(MemberList.None).
                ForMember(dest => dest.ImageFileName, opt => opt.MapFrom(src => src.Image.FileName));
                cfg.CreateMap<Post, PostIndexViewModel>(MemberList.None).
                ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.Votes.Where(v => v.Value == true).Count())).
                ForMember(dest => dest.DisLikes, opt => opt.MapFrom(src => src.Votes.Where(v => v.Value == false).Count())).
                ForMember(dest => dest.NumOfComments, opt => opt.MapFrom(src => src.Comments.Count));

                cfg.CreateMap<Post, PostDetailViewModel>(MemberList.None).
                ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.Votes.Where(v => v.Value == true).Count())).
                ForMember(dest => dest.DisLikes, opt => opt.MapFrom(src => src.Votes.Where(v => v.Value == false).Count())).
                ForMember(dest => dest.NumOfComments, opt => opt.MapFrom(src => src.Comments.Count)).
                ForMember(dest => dest.CurrentUserVote, opt => opt.MapFrom(src => src.Votes.Where(
                          v => v.VoterId == HttpContext.Current.User.Identity.GetUserId()).SingleOrDefault())); 

                cfg.CreateMap<Comment, CommentPartialViewModel>(MemberList.None).
                ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.Votes.Where(v => v.Value == true && v.CommentID == src.CommentID).Count())).
                ForMember(dest => dest.DisLikes, opt => opt.MapFrom(src => src.Votes.Where(v => v.Value == false && v.CommentID == src.CommentID).Count())).
                ForMember(dest => dest.NumOfComments, opt => opt.MapFrom(src => src.Comments.Count)).
                ForMember(dest => dest.CurrentUserVote, opt => opt.MapFrom(src => src.Votes.Where(
                          v => v.VoterId == HttpContext.Current.User.Identity.GetUserId()).SingleOrDefault()));               
                cfg.CreateMap<Comment, CommentDetailsViewModel>(MemberList.None);
                cfg.CreateMap<Comment, DeleteCommentViewModel>(MemberList.None);

                cfg.CreateMap<ApplicationUser, BloggerDetailViewModel>(MemberList.None).
                ForMember(dest => dest.Posts, opt => opt.Ignore()).
                ForMember(dest => dest.PostCount, opt => opt.MapFrom(src => src.Posts.Count)).
                ForMember(dest => dest.ImageFile, opt => opt.MapFrom(src => src.Image.FileName));
                cfg.CreateMap<ApplicationUser, BloggersViewModel>(MemberList.None).
                ForMember(dest => dest.PostCount, opt => opt.MapFrom(src => src.Posts.Count)).
                ForMember(dest => dest.ImageFile, opt => opt.MapFrom(src => src.Image.FileName));

                //************************* DTO's **********************************************
                cfg.CreateMap<PostCommentDTO,Comment > (MemberList.None).
                ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Comment));
                cfg.CreateMap<PutCommentDTO,Comment > (MemberList.None).
                ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Comment)); 
            });

            //call after initialization
            Mapper.AssertConfigurationIsValid();
        }

        public class LikeResolver : IValueResolver<Post, AuthorViewModel, int>
        {
            public int Resolve(Post source, AuthorViewModel destination, int member, ResolutionContext context)
            {
                return source.Votes.Where(v => v.Value == true).Count();
            }
        }
        public class DislikeResolver : IValueResolver<Post, AuthorViewModel, int>
        {
            public int Resolve(Post source, AuthorViewModel destination, int member, ResolutionContext context)
            {
                return source.Votes.Where(v => v.Value == false).Count();
            }
        }
    }
}