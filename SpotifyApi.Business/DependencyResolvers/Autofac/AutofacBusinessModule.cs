﻿using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using SpotifyApi.Business.Abstract;
using SpotifyApi.Business.Concrete;
using SpotifyApi.Core.Security;
using SpotifyApi.DataAccess.Abstract;
using SpotifyApi.DataAccess.Concrete.EntityFramework;

namespace SpotifyApi.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Servisler buraya eklenecek.
            builder.RegisterType<AlbumManager>().As<IAlbumService>();
            builder.RegisterType<EfAlbumDal>().As<IAlbumDal>();

            builder.RegisterType<PlaylistManager>().As<IPlaylistService>();
            builder.RegisterType<EfPlaylistDal>().As<IPlaylistDal>();

         

            builder.RegisterType<PlaylistFollowerManager>().As<IPlaylistFollowerService>();
            builder.RegisterType<EfPlaylistFollowerDal>().As<IPlaylistFollowerDal>();

            builder.RegisterType<EfUserDal>().As<IUserDal>();
            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserOperationClaim>().As<IUserOperationClaimDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            builder.RegisterType<EfUserFollowerDal>().As<IUserFollowerDal>();
            builder.RegisterType<UserFollowerManager>().As<IUserFollowerService>();

            builder.RegisterType<SongPoolManager>().As<ISongService>();

            builder.RegisterType<EfLibraryDal>().As<ILibraryDal>();
            builder.RegisterType<LibaryManager>().As<ILibaryService>();

            builder.RegisterType<EfFavouriteDal>().As<IFavouriteDal>();
            builder.RegisterType<FavouriteManager>().As<IFavouriteService>();


           
        }
    }
}
