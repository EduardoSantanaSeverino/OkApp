import { OkAppTemplatePage } from './app.po';

describe('OkApp App', function() {
  let page: OkAppTemplatePage;

  beforeEach(() => {
    page = new OkAppTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});

