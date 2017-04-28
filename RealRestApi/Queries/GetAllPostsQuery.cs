﻿using System.Threading.Tasks;
using RealRestApi.Models;
using Mapster;

namespace RealRestApi.Queries
{
    public class GetAllPostsQuery
    {
        private readonly BeautifulContext _context;
        private readonly PagedCollectionParameters _defaultPagingParameters;
        private readonly TypeAdapterConfig _typeAdapterConfig;
        private readonly string _endpoint;

        public GetAllPostsQuery(
            BeautifulContext context,
            PagedCollectionParameters defaultPagingParameters,
            TypeAdapterConfig typeAdapterConfig,
            string endpoint)
        {
            _context = context;
            _defaultPagingParameters = defaultPagingParameters;
            _typeAdapterConfig = typeAdapterConfig;
            _endpoint = endpoint;
        }

        public Task<PagedCollection<Post>> Execute(PagedCollectionParameters parameters)
        {
            var collectionFactory = new PagedCollectionFactory<Post>(PlaceholderLink.ToCollection(_endpoint));

            return collectionFactory.CreateFrom(
                _context.Posts.ProjectToType<Post>(_typeAdapterConfig),
                parameters.Offset ?? _defaultPagingParameters.Offset.Value,
                parameters.Limit ?? _defaultPagingParameters.Limit.Value);
        }
    }
}
