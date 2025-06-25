# --- build stage ---
FROM node:20-alpine AS build
WORKDIR /app

# copy the package files (optimizes caching)
COPY package*.json ./

# install dependencies
RUN npm install

# copy the rest of the code
COPY ./ ./

# build the app
RUN npm run build

# --- runtime stage ---
FROM nginx:alpine AS runtime
WORKDIR /usr/share/nginx/html

# clean the default nginx files
RUN rm -rf ./*

# copy the built app
COPY --from=build /app/dist ./

# copy custom nginx config
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
# start nginx
CMD ["nginx", "-g", "daemon off;"]