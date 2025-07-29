FROM node:22.12 AS build
WORKDIR /app 
COPY package.json package-lock.json ./
RUN npm install --include=dev --force
COPY . .
RUN npm run build --configuration=production
FROM nginx:alpine
COPY --from=build /app/dist/AppMonitoringService.UI/browser /usr/share/nginx/html
RUN chmod -R 755 /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]